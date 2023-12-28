using Newtonsoft.Json;

namespace yatracub.Models
{
    public class resultStatus
    {
        [JsonProperty(PropertyName = "Data")]
        public object Data { get; set; }
        public string message { get; set; }
        public int httpStatusCode { get; set; }
        public byte status { get; set; }

    }
}
