using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Application.Infrastructures;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Pages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModelBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<EmailTemplates> emailTemplates;
        private readonly ResellerConfig resellerConfig;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IOptions<ResellerConfig> resellerConfig,
            IStringLocalizer<EmailTemplates> emailTemplates)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.emailTemplates = emailTemplates;
            this.resellerConfig = resellerConfig.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required (ErrorMessage = "The {0} field is required.")]           
            [Display(Name = "UserName", Prompt ="UserName")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [EmailAddress]
            [Display(Name = "Email", Prompt="Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password", Prompt="Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password", Prompt="Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = Url.Content("/Identity/Account/RegisterConfirmation");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { AppId = resellerConfig.AppId, UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, emailTemplates["FG NetService - Account Activation"],
                        string.Format(emailTemplates[@"Thank you for account registration.<br/>Please click <a href='{0}'>here</a> to activate your account.<br/><br/>------------------------------------<br/>FG NetService Customer Support<br/><br/>Email: <a href='mailto:info@fgnetservice.com'>info@fgnetservice.com</a><br/>------------------------------------"], 
                        HtmlEncoder.Default.Encode(callbackUrl))
                       );
                                       
                    return Redirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
