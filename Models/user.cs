using System.Numerics;

namespace yatracub.Models
{
    public class user
    {
        public long linkid { get; set; }
        public string emailid { get; set; }
        public string username { get; set; }
        public long mobile { get; set; }
        public string password { get; set; }
        public byte isactive { get; set; }
        public byte isdelete { get; set; }
        //public byte isUpdate { get; set; }
        //public DateTime dob { get; set; }
        //public DateTime createdon { get; set; }
    }


    public class otphits
    {
    public long mobile {get; set; }
    public string emailid { get; set; }
    public int otp { get; set; }
    public int isdelete {get; set; }
    }
}
