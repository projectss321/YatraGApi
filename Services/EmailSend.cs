using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class EmailSend : IEmailSend
    {
        private readonly IEmailSendRepo _IEmailSendRepo;
        public EmailSend(IEmailSendRepo IEmailSendRepo)
        {
            _IEmailSendRepo = IEmailSendRepo;
        }
        public Task SendEmailAsync(MailRequest email)
        {
            return _IEmailSendRepo.SendEmailAsync(email);
        }
    }
}
