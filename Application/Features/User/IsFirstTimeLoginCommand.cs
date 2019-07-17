using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class IsFirstTimeLoginCommand : IRequest<IsFirstTimeLoginCommandResponse>
    {
    }

    public class IsFirstTimeLoginCommandResponse : BaseResponse
    {
    }

    public class IsFirstTimeLoginCommandHandler : IRequestHandler<IsFirstTimeLoginCommand, IsFirstTimeLoginCommandResponse>
    {
        public Task<IsFirstTimeLoginCommandResponse> Handle(IsFirstTimeLoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
