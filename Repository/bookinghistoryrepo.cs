using Microsoft.Data.SqlClient;
using System.Reflection;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Shared;
using yatracub.Shared.Interface;
using yatracub.Util;

namespace yatracub.Repository
{
    public class bookinghistoryrepo : IbookingHistoryrepo
    {
        private readonly IdbContext _dbConnection;
        private readonly IcommonFunction _commonfunction;

        public bookinghistoryrepo(IdbContext dbcontext, IcommonFunction commonfunction)
        {
            _dbConnection = dbcontext;
            _commonfunction = commonfunction;
        }
        public string saveUpdateBookings(bookinghistory booking)
        {

            var con = new SqlConnection(_dbConnection.getConnectionString());

            try
            {
                SqlCommand cmd = new SqlCommand();
                var result = "";
                booking.linkid = UtilFunction.GetLinkid();
                resultStatus rstatus = new resultStatus();

                SqlParameter[] parameter =
                {
                    cmd.Parameters.AddWithValue("@linkid", booking.linkid),
                    cmd.Parameters.AddWithValue("@userlinkid", booking.userlinkid),
                    cmd.Parameters.AddWithValue("@fromplacelinkid", booking.fromplacelinkid),
                    cmd.Parameters.AddWithValue("@toplacelinkid", booking.toplacelinkid),
                    cmd.Parameters.AddWithValue("@fromdate", booking.fromdate),
                    cmd.Parameters.AddWithValue("@todate", booking.todate),
                    cmd.Parameters.AddWithValue("@bookingstatus", booking.bookingstatus),
                    cmd.Parameters.AddWithValue("@isactive", booking.isactive),
                    cmd.Parameters.AddWithValue("@isconfirmed", booking.isconfirmed),
                    cmd.Parameters.AddWithValue("@bookingamount", booking.bookingamount),
                    cmd.Parameters.AddWithValue("@paymentlinkid", booking.paymentlinkid),
                    cmd.Parameters.AddWithValue("@vehiclelinkid", booking.vehiclelinkid),
                    cmd.Parameters.AddWithValue("@driverlinkid", booking.driverlinkid),
                    cmd.Parameters.AddWithValue("@managerlinkid", booking.managerlinkid),
                    cmd.Parameters.AddWithValue("@partnerlinkid", booking.partnerlinkid)
                };
                cmd.Connection = con;
                cmd.CommandText = _commonfunction.getInsertQuery(parameter, "bookinghistory");
                con.Open();
                var status = cmd.ExecuteNonQuery();
                con.Close();

                if (status != 0)
                {
                    result = _commonfunction.successResult(rstatus);
                }
                else
                {
                    result = _commonfunction.errorResult();
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
