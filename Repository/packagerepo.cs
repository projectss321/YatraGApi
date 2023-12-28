using Microsoft.Data.SqlClient;
using System.Data;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Shared;
using yatracub.Shared.Interface;

namespace yatracub.Repository
{
    public class packagerepo : IpackageRepo
    {
        private readonly IdbContext _dbconnection;
        private readonly IcommonFunction _commonFunction;
        public packagerepo(IdbContext dbconnection, IcommonFunction commonFunction)
        {
            _dbconnection = dbconnection;
            _commonFunction = commonFunction;
        }
        public string getPackage()
        {
            resultStatus rstatus = new resultStatus();
            var con = new SqlConnection(_dbconnection.getConnectionString());
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pm.linkid as packageLinkid, pm.partnerlinkid as partnerlinkid, pm.vehiclelinkid as packagevehiclelinkid, pm.ratepkm as ratepkm, vm.linkid as vehiclelinkid , vm.seater as seater, vm.vehiclename as vehiclename from packagemaster (nolock) pm inner join vehiclemaster (nolock) vm on pm.vehiclelinkid = vm.linkid";
                con.Open();

                var status = cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds,"packagemaster");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rstatus.Data = ds.Tables[0];
                  return _commonFunction.successResult(rstatus);
                }
                else
                {
                    return _commonFunction.errorResult("Package not available");
                }
            }
            catch (Exception ex)
            {
                return _commonFunction.errorResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            
        }
    }
}
