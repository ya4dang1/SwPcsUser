using Core.Infrastructures;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig emailConfig;
        private readonly IHostingEnvironment env;

        public EmailSender(IOptions<EmailConfig> emailConfig, IHostingEnvironment env)
        {
            this.emailConfig = emailConfig.Value;
            this.env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            if (!emailConfig.Enable || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emailConfig.MailServer))
                return;


            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(emailConfig.FromName, emailConfig.From));
            mailMessage.To.Add(new MailboxAddress(email));
            mailMessage.Subject = subject;

            if (!env.IsProduction())
            {
                mailMessage.Subject += $" ({env.EnvironmentName})";
            }

            mailMessage.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(emailConfig.MailServer, emailConfig.MailPort, MailKit.Security.SecureSocketOptions.Auto);
                    if (!String.IsNullOrEmpty(emailConfig.UserName))
                    {
                        await smtpClient.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);
                    }

                    await smtpClient.SendAsync(mailMessage);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
