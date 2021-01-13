using HostManager.Contracts;
using HostManager.Settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SmtpSettings smtpSettings;
        private readonly IWebHostEnvironment _env;
        public EmailService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            smtpSettings = new SmtpSettings();
            _config.GetSection(nameof(SmtpSettings)).Bind(smtpSettings);
            _env = env;
        }
        public async Task SendMailAsync(string email, string subject, string body, string path)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = subject;
                var multipart = new Multipart("mixed");
                var streams = new List<Stream>();

                if (path != null)
                {
                    var stream = File.OpenRead(path);
                    var attachment = new MimePart(MimeTypes.GetMimeType(path))
                    {
                        Content = new MimeContent(stream),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(path)
                    };
                    multipart.Add(attachment);
                    streams.Add(stream);
                }


                var html = new TextPart("html")
                {
                    Text = body,
                };

                multipart.Add(html);
                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, true);
                    await client.AuthenticateAsync(smtpSettings.UserName, smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Console.WriteLine("Email sent");
                foreach (var stream in streams)
                {
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Email was not send {e.Message}");
            }
        }
    }
}