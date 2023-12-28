using yatracub.Models;
using System.Threading.Tasks;

namespace yatracub.Services.Interface
{
    public interface IEmailSend
    {
        Task SendEmailAsync(MailRequest email);
    }
}
