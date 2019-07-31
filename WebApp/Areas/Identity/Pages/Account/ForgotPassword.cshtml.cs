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
using Microsoft.Extensions.Options;
using WebApp.Pages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModelBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<ResellerConfig> resellerConfig;
        private readonly IStringLocalizer<EmailTemplates> emailTemplates;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, 
            IOptions<ResellerConfig> resellerConfig, IStringLocalizer<EmailTemplates> emailTemplates)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            this.resellerConfig = resellerConfig;
            this.emailTemplates = emailTemplates;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "The {0} field is required.")]
            [Display(Name="UserName", Prompt ="UserName")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [EmailAddress(ErrorMessage = "The {0} field is not a valid e-mail address.")]
            [Display(Name = "Email", Prompt = "Email")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.UserName);

                if (user == null || user.Email != Input.Email || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                if(user.AppId == resellerConfig.Value.AppId)
                {
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        emailTemplates["FG NetService - Reset Password"],
                        String.Format(emailTemplates[@"Please click <a href='{0}'>here</a> to reset your password.<br/><br/>------------------------------------<br/>FG NetService Customer Support<br/><br/>Email: <a href='mailto:info@fgnetservice.com'>info@fgnetservice.com</a><br/>------------------------------------"],
                            HtmlEncoder.Default.Encode(callbackUrl))
                        );

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid user");
                }
            }

            return Page();
        }
    }
}
