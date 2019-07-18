using Application.Enumerations;
using Core.Libraries;
using Core.Models;
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
        public string UserId { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public string AddressType { get; set; }
                
        public string Address { get; set; }
                
        public string City { get; set; }

        public string Region { get; set; }
                
        public string Zip { get; set; }
                
        public bool IsDeliveryYN { get; set; }
                
        public string Mobile { get; set; }
                
        public string Email { get; set; }
                
        public bool CardRequestYN { get; set; }

        public string CardPersonName { get; set; }
                
        public string FundSource { get; set; }
                
        public string Industry { get; set; }
                
        public string Subindustry { get; set; }

        public string Comments { get; set; }
                
        public string IDValue { get; set; }
                
        public string IDType { get; set; }
                
        public DateTime IDIssuanceDate { get; set; }
                
        public DateTime IDExpiryDate { get; set; }

        public Guid IDFileId { get; set; }

        public FileLibrary IDFile { get; set; }
                
        public string DeliveryCountry { get; set; }

        public UserStatus Status { get; set; }

    }
}
