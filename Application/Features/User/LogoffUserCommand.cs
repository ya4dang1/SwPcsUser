using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class LogoffUserCommand : IRequest<LogoffUserCommandResponse>
    {
    }

    public class LogoffUserCommandResponse : BaseResponse
    {
    }

    public class LogoffUserCommandHandler : IRequestHandler<LogoffUserCommand, LogoffUserCommandResponse>
    {
        public Task<LogoffUserCommandResponse> Handle(LogoffUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
