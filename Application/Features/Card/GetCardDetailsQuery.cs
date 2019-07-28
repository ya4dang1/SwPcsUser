using Application.Models;
using Application.Services;
using Core.Libraries;
using EmyralSystems;
using EmyralSystems.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Card
{
    public class GetCardDetailsQuery : IRequest<GetCardDetailsQueryResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetCardDetailsQueryResponse : BaseResponse
    {
        public UserCard Card { get; set; }
        public Dictionary<string,double> Balance { get; set; }
        public List<CardTransaction> Transaction { get; set; }


    }

    public class GetCardInformationQueryHandler : IRequestHandler<GetCardDetailsQuery, GetCardDetailsQueryResponse>
    {
        private readonly LoginUser loginUser;
        private readonly ApplicationDbContext dbContext;
        private readonly PcsDbContext pcsDbContext;

        public GetCardInformationQueryHandler(LoginUser loginUser, ApplicationDbContext dbContext, PcsDbContext pcsDbContext)
        {
            this.loginUser = loginUser;
            this.dbContext = dbContext;
            this.pcsDbContext = pcsDbContext;
        }

        public async Task<GetCardDetailsQueryResponse> Handle (GetCardDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCardDetailsQueryResponse();
            var user = await loginUser.GetAsync();

            if (user != null)
            {
                var userCard = await dbContext.UserCards.FirstOrDefaultAsync(w => w.Id == request.Id && w.User == user);
                if (userCard != null)
                {
                    response.Card = userCard;
                    var pcsCard = await pcsDbContext.Card.FirstOrDefaultAsync(fd => fd.CardNumber == userCard.CardNumber);
                    if (pcsCard != null)
                    {
                        pcsDbContext.Entry(pcsCard).Collection(c => c.CardTransaction).Load();
                        response.Balance = JsonConvert.DeserializeObject<Dictionary<string, double>>(pcsCard.Balance);
                        response.Transaction = pcsCard.CardTransaction.Where(w => w.Amount > 0 && w.TranType != "BALINQ").ToList();
                    }
                    else
                        response.AddError("Invalid card");                    
                }
                else
                    response.AddError("Invalid card");
            }
            else
                response.AddError("Invalid user");
            
            return response;
        }
    }
}
