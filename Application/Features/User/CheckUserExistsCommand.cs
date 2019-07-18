using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Core.Models;

namespace Application.Features.User
{
    public class CheckUserExistsCommand : IRequest<CheckUserExistsCommandResponse>
    {
        public string UserName { get; set; }
    }

    public class CheckUserExistsCommandResponse : BaseResponse
    {
        public bool Exists { get; set; }        
    }

    public class CheckUserExistsCommandHandler : IRequestHandler<CheckUserExistsCommand, CheckUserExistsCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CheckUserExistsCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<CheckUserExistsCommandResponse> Handle(CheckUserExistsCommand request, CancellationToken cancellationToken)
        {
            var response = new CheckUserExistsCommandResponse();
            var user = this.userManager.FindByNameAsync(request.UserName);
            response.Exists = user != null;
            return Task.FromResult(response);
        }
    }
}
