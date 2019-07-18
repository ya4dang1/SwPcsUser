using Core.Libraries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResponse>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandResponse : BaseResponse
    {
        public bool Succeeded { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
    {
        private readonly UserManager<IdentityUser> userManager;

        public ChangePasswordCommandHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ChangePasswordCommandResponse> Handle (ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new ChangePasswordCommandResponse();
            response.Succeeded = true;

            var user = await userManager.FindByNameAsync(request.UserName);

            if (user == null)            
                response.Succeeded = false;
            else
            {
                var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                response.Succeeded = result.Succeeded;
            }

            if (!response.Succeeded)
                response.Errors.Add("Change password unsucessful");            

            return response;
           
        }
    }
}
