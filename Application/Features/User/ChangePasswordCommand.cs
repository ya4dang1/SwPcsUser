using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResponse>
    {

    }

    public class ChangePasswordCommandResponse : BaseResponse
    {
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
    {
        public Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
