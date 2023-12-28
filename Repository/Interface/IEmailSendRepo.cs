using yatracub.Models;

namespace yatracub.Repository.Interface
{   
    public interface IEmailSendRepo
    {
        public Task SendEmailAsync(MailRequest mailRequest);
    }
}
