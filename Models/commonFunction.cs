using System.Net;

namespace yatracub.Models
{
    public class httpStatus
    {
        public int status { get; set; }
        public string message { get; set; }
        public HttpStatusCode code { get; set; }
    }
}
