using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using yatracub.Models;
using yatracub.Services.Interface;

namespace yatracub.Controllers
{
    public class UtilController : BaseController
    {
        private readonly IApplicationTypeServices _applicationTypeServices;
        public UtilController(IApplicationTypeServices applicationTypeServices) {
            _applicationTypeServices = applicationTypeServices;
        }
        [HttpGet("SystemIp")]
        public async Task<AllsystemIp> GetSystemIp() {

            var ips = new AllsystemIp();
            try
            {
                ips.IP = _applicationTypeServices.GetSystemIp();
                return ips;
            }
            catch (Exception ex)
            {
                return ips;
            }

        }
    }
}
