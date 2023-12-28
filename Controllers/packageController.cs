using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using yatracub.Models;
using yatracub.Services.Interface;

namespace yatracub.Controllers
{
    public class packageController : BaseController
    {
        private readonly Ipackageservice _pakageservice;
        public packageController(Ipackageservice pakageservice)
        {
            _pakageservice = pakageservice;
        }

        [HttpGet("getPackage")]
        public IActionResult getPackage()
        {
            try
            {
               var rstatus = _pakageservice.getPackage();
                return Ok(rstatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }
        }
    }
}
