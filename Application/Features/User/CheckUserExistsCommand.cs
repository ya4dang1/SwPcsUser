using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;

namespace Application.Features.User
{
    public class CheckUserExistsCommand : IRequest<CheckUserExistsCommandResponse>
    {
        public string UserId { get; set; }
    }

    public class CheckUserExistsCommandResponse : BaseResponse
    {
        public bool Success { get; set; }
    }

    public class CheckUserExistsCommandHandler : IRequestHandler<CheckUserExistsCommand, CheckUserExistsCommandResponse>
    {   

        public CheckUserExistsCommandHandler()
        {
        }

        public Task<CheckUserExistsCommandResponse> Handle(CheckUserExistsCommand request, CancellationToken cancellationToken)
        {
            var response = new CheckUserExistsCommandResponse();           

            try
            {                
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.AddError(ex);
            }

            return Task.FromResult(response);
        }
    }
}
