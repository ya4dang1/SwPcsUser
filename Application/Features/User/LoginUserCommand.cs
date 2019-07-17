using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandResponse : BaseResponse
    {
        public bool IsValid { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {   

        public LoginUserCommandHandler()
        {   
        }

        public Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            LoginUserCommandResponse response = new LoginUserCommandResponse();

            response.IsValid = true;

            return Task.FromResult(response);
        }
    }
}
