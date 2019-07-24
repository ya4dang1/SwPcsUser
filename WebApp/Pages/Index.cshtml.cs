using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationService authorizationService;       

        public IndexModel(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;            
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if(!(await authorizationService.AuthorizeAsync(HttpContext.User, "IsApproved")).Succeeded)
            {
                return Redirect("User/UpdateProfile");
            }

            return Redirect("/Card");
        }
    }
}
