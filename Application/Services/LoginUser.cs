using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginUser
    {
        private readonly HttpContext httpContext;
        private readonly UserManager<ApplicationUser> userManager;

        public LoginUser(HttpContext httpContext, UserManager<ApplicationUser> userManager)
        {
            this.httpContext = httpContext;
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetAsync() => await userManager.GetUserAsync(httpContext.User);
    }
}
