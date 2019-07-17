using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;

namespace Application.Features.User
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

        public RegisterNewUserCommandHandler()
        {           
        }

        public Task<RegisterNewUserCommandResponse> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var response = new RegisterNewUserCommandResponse();

            return Task.FromResult(response);
        }
    }
}
