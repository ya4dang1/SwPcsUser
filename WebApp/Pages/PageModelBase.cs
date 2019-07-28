using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
	public class PageModelBase : PageModel
	{
        #pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods.
        [HttpPost]
        #pragma warning restore MVC1002 // Route attributes cannot be applied to page handler methods.
        public IActionResult OnPostSetLanguageAsync(string culture, string returnUrl)
		{
            if(!(culture is String))
            {
                culture = string.Empty;
            }
            if (!(returnUrl is String))
            {
                returnUrl = "/Identity/Account/Login";
            }
            Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return LocalRedirect(returnUrl);
		}
	}
}
