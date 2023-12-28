using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Shared;

namespace yatracub.Repository
{
    public class EmailSendRepo : IEmailSendRepo
    {
        private readonly IdbContext _dbconnection;
        private readonly MailSettings _mailSettings;
        private IConfiguration _configuration;
        public EmailSendRepo(IdbContext dbconnection, MailSettings mailSettings, IConfiguration configuration)
        {
            _dbconnection = dbconnection;
            _mailSettings = mailSettings;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, Convert.ToInt32(_mailSettings.Port), SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
           
    }
}

