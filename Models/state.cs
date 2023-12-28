namespace yatracub.Models
{
    public class state
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public long linkid { get; set; }
        public string name { get; set; }
        public string state_code { get; set; }
        public string type { get; set; }
        public int isactive { get; set; }
        public int isdelete { get; set; }
    }

    public class city
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public long linkid { get; set; }
        public string name { get; set; }
        public int state_id { get; set; }
        public string state_code { get; set; }
        public int isactive { get; set; }
        public int isdelete { get; set; }
    }

    public class citydetail
    {
       // public long linkid {get; set; }
       // public long citylinkid {get; set; }
        //public dynamic img {get; set; }
        //public string img2 {get; set; }
        //public string img3 {get; set; }
        //public string img4 {get; set; }
        //public string img5 {get; set; }
        //public string img6 {get; set; }
        //public string img7 {get; set; }
        //public string img8 {get; set; }
        //public string img9 {get; set; }
        //public string img10 {get; set; }
        //public string img11 {get; set; }
        //public string img12 {get; set; }
        //public string img13 {get; set; }
        //public string header {get; set; }
        //public string subheader {get; set; }
        //public string subheader2 {get; set; }
        //public string subheader3 {get; set; }
        //public string content {get; set; }
        //public string neareststationname {get; set; }
        //public int stationdistance {get; set; }
        //public string nearestbusname {get; set; }
        //public int busdistance {get; set; }
        //public string nearestairport {get; set; }
        //public int airportdistance { get; set; }
        public IFormFile files { get; set; }
    }

    public class FileAttachment
    {
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
    }
}
