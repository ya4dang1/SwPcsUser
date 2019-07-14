using OmniPay;
using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Helper;

namespace Application.Modules.User
{
    public class RegisterNewUserCommand : IRequest<RegisterNewUserCommandResponse>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        public string Region { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public bool IsDeliveryYN { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool CardRequestYN { get; set; }

        public string CardPersoName { get; set; }

        [Required]
        public string FundSource { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public string Subindustry { get; set; }

        public string Comments { get; set; }

        [Required]
        public string IDValue { get; set; }

        [Required]
        public string IDType { get; set; }

        [Required]
        public DateTime IDIssuanceDate { get; set; }

        [Required]
        public DateTime IDExpiryDate { get; set; }

        [Required]
        public string DeliveryCountry { get; set; }
    }

    public class RegisterNewUserCommandResponse : BaseResponse
    {
        public bool Success { get; set; }
        public string Result { get; set; }
        public string NewPassword { get; set; }
    }

    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, RegisterNewUserCommandResponse>
    {
        private readonly WSCrystalPaymentsSvcSoapClient ws;

        public RegisterNewUserCommandHandler(WSCrystalPaymentsSvcSoapClient ws)
        {
            this.ws = ws;
        }

        public Task<RegisterNewUserCommandResponse> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var response = new RegisterNewUserCommandResponse();
            var hMACHelper = new HMACHelper("registernewuser");

            try
            {
                CMsgSecurity cMsgSecurity = new CMsgSecurity
                {
                    MessageFormat = MsgFormat.FORMAT_1,
                    MessageHMAC = hMACHelper.ToString(),
                    MobileUUID = hMACHelper.GetMobileUUID(),
                    TranDateTime = hMACHelper.GetTranDateTime()
                };

                CAddressInfo[] cAddressInfos = new CAddressInfo[]
                {
                    new CAddressInfo
                    {
                        Address = request.Address,
                        AddressType = request.AddressType,
                        City = request.City,
                        Country = request.DeliveryCountry,
                        Region = request.Region,
                        Zip = request.Zip,
                        IsDeliveryYN = request.IsDeliveryYN ? "Y" : "N"
                    }
                };

                //Optional
                CSocialMedia[] cSocialMedias = new CSocialMedia[]
                {
                };

                ///TODO: Add Image in
                CCardholderIDInfo[] cCardholderIDInfos = new CCardholderIDInfo[]
                {
                    new CCardholderIDInfo
                    {
                        Idvalue = request.IDValue,
                        Idtype = request.IDType,
                        Idissuancedate = hMACHelper.GetDateTimeString(request.IDIssuanceDate),
                        Idexpirydate = hMACHelper.GetDateTimeString(request.IDExpiryDate),
                    }
                };

                CAuditHistory cAuditHistory = new CAuditHistory
                {
                };

                var result = ws.RegisterNewUserAsync(
                    cMsgSecurity,
                    request.UserId,
                    request.LastName,
                    request.FirstName,
                    request.MiddleName,
                    hMACHelper.GetDateTimeString(request.Birthday),
                    cAddressInfos,
                    request.Mobile,
                    request.Email,
                    request.CardRequestYN ? "Y" : "N",
                    request.CardPersoName,
                    request.FundSource,
                    request.Industry,
                    request.Subindustry,
                    request.Comments,
                    cSocialMedias,
                    cCardholderIDInfos,
                    request.DeliveryCountry,
                    cAuditHistory
                    ).Result;

                if (result.Body.RegisterNewUserResult == null)
                {
                    response.Success = false;
                }
                else
                {
                    response.Success = result.Body.RegisterNewUserResult.bSuccess;
                    response.NewPassword = result.Body.RegisterNewUserResult.NewPassword;
                    response.Result = result.Body.RegisterNewUserResult.ResultString;
                }
            }
            catch (Exception ex)
            {
                response.AddError(ex);
            }

            return Task.FromResult(response);
        }
    }
}
