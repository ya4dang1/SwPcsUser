using Application.Models;
using Application.Services;
using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Card
{
    public class GetCardsQuery: IRequest<GetCardsQueryResponse>
    {
    }

    public class GetCardsQueryResponse: BaseResponse
    {
        public List<UserCard> Cards { get; set; }
    }

    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, GetCardsQueryResponse>
    {
        private readonly LoginUser loginUser;
        private readonly ApplicationDbContext dbContext;

        public GetCardsQueryHandler(LoginUser loginUser, ApplicationDbContext dbContext)
        {
            this.loginUser = loginUser;
            this.dbContext = dbContext;
        }

        public async Task<GetCardsQueryResponse> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCardsQueryResponse();
            var user = await loginUser.GetAsync();

            if (user != null)
                response.Cards = dbContext.UserCards.Where(w => w.User == user).ToList();
            else
                response.AddError("Invalid user");

            return response;            
        }
    }
}
