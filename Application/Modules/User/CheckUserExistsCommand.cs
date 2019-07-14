using OmniPay;
using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Helper;

namespace Application.Modules.User
{
    public class CheckUserExistsCommand: IRequest<CheckUserExistsCommandResponse>
    {
        public string UserId { get; set; }
    }

    public class CheckUserExistsCommandResponse: BaseResponse
    {
        public bool Success { get; set; }
    }

    public class CheckUserExistsCommandHandler : IRequestHandler<CheckUserExistsCommand, CheckUserExistsCommandResponse>
    {        
        private readonly WSCrystalPaymentsSvcSoapClient wS;

        public CheckUserExistsCommandHandler(WSCrystalPaymentsSvcSoapClient wS)
        {            
            this.wS = wS;
        }

        public Task<CheckUserExistsCommandResponse> Handle(CheckUserExistsCommand request, CancellationToken cancellationToken)
        {
            var response = new CheckUserExistsCommandResponse();
            var hMACHelper = new HMACHelper("checkuserexits");

            try
            {
                hMACHelper.AddProperty("puserid", request.UserId);

                var cMsgSecurity = new CMsgSecurity
                {
                    MessageFormat = MsgFormat.FORMAT_1,
                    MessageHMAC = hMACHelper.ToString(),
                    TranDateTime = hMACHelper.GetTranDateTime(),
                    MobileUUID = hMACHelper.GetMobileUUID()
                };

                var result = wS.CheckUserExistsAsync(cMsgSecurity,request.UserId).Result;
                response.Success = result.Body.CheckUserExistsResult;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.AddError(ex);
            }

            return Task.FromResult(response);
        }
    }
}
