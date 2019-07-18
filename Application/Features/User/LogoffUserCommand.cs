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
    public class LogoffUserCommand : IRequest<Unit>
    {
    }

    public class LogoffUserCommandHandler : IRequestHandler<LogoffUserCommand, Unit>
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public LogoffUserCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoffUserCommand request, CancellationToken cancellationToken)
        {            
            await signInManager.SignOutAsync();
            return Unit.Value;
        }
    }
}
