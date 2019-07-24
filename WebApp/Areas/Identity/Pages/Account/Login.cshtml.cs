using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Core.Models;
using WebApp.Pages;
using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Application.Infrastructures;
using Application.Enumerations;
using System.IO;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModelBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly ResellerConfig resellerConfig;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            IOptions<ResellerConfig> resellerConfig,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.resellerConfig = resellerConfig.Value;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]           
            [Display(Prompt = "UserName")]
            public string UserName { get; set; }

            [Required]
            [Display(Prompt = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(fd => fd.UserName == Input.UserName && fd.AppId == resellerConfig.AppId);
                if(user != null)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");

                        var userProfile = await dbContext.UserProfiles.FirstOrDefaultAsync(fd => fd.User == user);

                        var claims = await userManager.GetClaimsAsync(user);
                        await userManager.RemoveClaimsAsync(user, claims);

                        if(userProfile == null || userProfile.IsPending())                        
                            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Approved", "False"));                        
                        else                     
                            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Approved", "True"));                      

                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }                  
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
