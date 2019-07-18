using Core.Libraries;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandResponse : BaseResponse
    {
        public SignInResult Result { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            LoginUserCommandResponse response = new LoginUserCommandResponse();            
            response.Result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            return response;
        }
    }
}
