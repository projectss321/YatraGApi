using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using yatracub.Services.Interface;

namespace yatracub.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class EmailController : BaseController
    {
        private readonly IEmailSend _IemailService;

        public EmailController(IEmailSend IemailService)
            {
            _IemailService = IemailService;
            }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await _IemailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
