namespace yatracub.Models
{
    public class bookinghistory
    {
        public long linkid { get; set; }
        public long userlinkid { get; set; } = 9551212154;
        public long fromplacelinkid { get; set; }
        public long toplacelinkid { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public byte bookingstatus { get; set; } = 0;
        public byte isactive { get; set; } = 0;
        public byte isconfirmed { get; set; } = 0;
        public int bookingamount { get; set; } = 45000;
        public long paymentlinkid { get; set; } = 9551212157;
        public long vehiclelinkid { get; set; } = 9551212158;
        public long driverlinkid { get; set; } = 9551212159;
        public long managerlinkid { get; set; } = 9551212160;
        public long partnerlinkid { get; set; } = 9551212161;
    }
}
