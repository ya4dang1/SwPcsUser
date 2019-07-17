using Core.Libraries;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class UserProfile : BaseModel
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        public IdentityUser User { get; set; }

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
}
