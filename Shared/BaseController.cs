using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace yatracub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
