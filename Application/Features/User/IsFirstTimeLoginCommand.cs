using Application.Enumerations;
using Core.Libraries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class IsFirstTimeLoginCommand : IRequest<IsFirstTimeLoginCommandResponse>
    {
        public string UserName { get; set; }
    }

    public class IsFirstTimeLoginCommandResponse : BaseResponse
    {
        public bool IsFirstTimeLogin { get; set; }
    }

    public class IsFirstTimeLoginCommandHandler : IRequestHandler<IsFirstTimeLoginCommand, IsFirstTimeLoginCommandResponse>
    {
        private readonly ApplicationDbContext dbContext;

        public IsFirstTimeLoginCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IsFirstTimeLoginCommandResponse> Handle(IsFirstTimeLoginCommand request, CancellationToken cancellationToken)
        {
            var response = new IsFirstTimeLoginCommandResponse {  IsFirstTimeLogin = true};
            
            var user = await dbContext.Users.FirstOrDefaultAsync(fd => fd.NormalizedUserName == request.UserName.ToUpper());
            if(user != null)
            {
                var userProfile = dbContext.UserProfiles.FirstOrDefault(fd => fd.UserId == user.Id);
                if(userProfile != null)
                {
                    response.IsFirstTimeLogin = userProfile.Status == UserStatus.Pending;
                }
            }
            else
            {
                response.AddError("No user found");                
            }

            return response;
        }
    }
}
