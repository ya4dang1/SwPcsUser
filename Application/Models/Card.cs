using Core.Libraries;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class UserCard: BaseModel
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        public int PIN { get; set; }

    }
}
