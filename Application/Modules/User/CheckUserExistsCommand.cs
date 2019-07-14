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
        private readonly WSCrystalPaymentsSvcSoapClient wsClient;

        public CheckUserExistsCommandHandler(WSCrystalPaymentsSvcSoapClient wsClient)
        {
            this.wsClient = wsClient;
        }

        public Task<CheckUserExistsCommandResponse> Handle(CheckUserExistsCommand request, CancellationToken cancellationToken)
        {
            var response = new CheckUserExistsCommandResponse();
            var hMACHelper = new HMACHelper("checkuserexits");

            hMACHelper.AddProperty("puserid", request.UserId);

            try
            {
                var result = wsClient.CheckUserExistsAsync(
                    new CMsgSecurity
                    {
                        MessageFormat = MsgFormat.FORMAT_1,
                        MessageHMAC = hMACHelper.ToString(),
                        TranDateTime = hMACHelper.GetTranDateTime(),
                        MobileUUID = hMACHelper.GetMobileUUID()
                    },
                    request.UserId
                ).Result;

                response.Success = result.Body.CheckUserExistsResult;
            }
            catch(Exception ex)
            {
                response.AddError(ex);
            }

            return Task.FromResult(response);
        }
    }
}
