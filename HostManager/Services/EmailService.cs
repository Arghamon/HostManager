using HostManager.Contracts;
using HostManager.Models;
using HostManager.Settings;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SmtpSettings smtpSettings;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
            smtpSettings = new SmtpSettings();
            _config.GetSection(nameof(SmtpSettings)).Bind(smtpSettings);
        }
        public async Task SendMailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("Ioane", email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body,
                };

                using(var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, true);
                    await client.AuthenticateAsync(smtpSettings.UserName, smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Console.WriteLine("Email sent");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Email was not send {e.Message}");
            }
        }
    }
}
