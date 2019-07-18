using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string AppId { get; set; }
    }
}
