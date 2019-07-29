using Application.Services;
using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Card
{
    public class UnregisterCardCommand: IRequest<UnregisterCardCommandResponse>
    {
        public Guid Id { get; set; }
    }

    public class UnregisterCardCommandResponse: BaseResponse
    {
    }

    public class UnregisterCardCommandHandler : IRequestHandler<UnregisterCardCommand, UnregisterCardCommandResponse>
    {
        private readonly LoginUser loginUser;
        private readonly ApplicationDbContext dbContext;

        public UnregisterCardCommandHandler(LoginUser loginUser, ApplicationDbContext dbContext)
        {
            this.loginUser = loginUser;
            this.dbContext = dbContext;
        }

        public async Task<UnregisterCardCommandResponse> Handle(UnregisterCardCommand request, CancellationToken cancellationToken)
        {
            var response = new UnregisterCardCommandResponse();
            var user = await loginUser.GetAsync();
            if (user != null)
            {
                var userCard = await dbContext.UserCards.FindAsync(request.Id);
                if (userCard.User == user)
                {
                    dbContext.Remove(userCard);
                    await dbContext.SaveChangesAsync();
                }
                else
                    response.AddError("Unauthorize card user");

            }
            else
                response.AddError("Invalid user");
            
            return response;
        }
    }
}
