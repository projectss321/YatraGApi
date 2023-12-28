using Hangfire;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Shared;
using yatracub.Shared.Interface;
using yatracub.Util;
using static System.Net.WebRequestMethods;

namespace yatracub.Repository
{
    public class userrepo : Iuserrepo
    {
        private IdbContext _dbConnection;
        private readonly IHostEnvironment _env;
        private readonly IcommonFunction _commonfunction;
        private readonly IEmailSendRepo _IEmailSendRepo;

        public userrepo(IdbContext dbcontext, IcommonFunction commonfunction, IEmailSendRepo iEmailSendRepo, IHostEnvironment env)
        {
            _dbConnection = dbcontext;
            _commonfunction = commonfunction;
            _IEmailSendRepo = iEmailSendRepo;
            _env = env;
        }
        public string GetUser()
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                DataTable dt = new DataTable();

                SqlCommand cmd = new SqlCommand("select * from usermaster nolock", con);
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                var result = dt.AsEnumerable().Select(i =>
                {
                    return i.Table;
                }).FirstOrDefault();

                con.Close();

                return JsonConvert.SerializeObject(result);

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

        public string saveUpdateUser(user user)
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {

                SqlCommand cmd = new SqlCommand();
                dynamic result = "";
                resultStatus rstatus = new resultStatus();
                con.Open();

                var userlist = this.getuserbyId(user);
                dynamic convertObj = JsonConvert.DeserializeObject(userlist);
                if (userlist != null && userlist != "" && convertObj.Count > 0)
                {
                    foreach (var item in convertObj)
                    {
                        if (item.isactive == 0)
                        {
                            return _commonfunction.successResult(rstatus, "User is not activate");
                            //return JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            return _commonfunction.successResult(rstatus, "User is already exist");
                           // return JsonConvert.SerializeObject(result);
                        }
                    }
                    result = _commonfunction.errorResult();
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    //SqlParameter Parameters = new SqlParameter();
                    user.linkid = UtilFunction.GetLinkid();
                    object[] parameter = {
                cmd.Parameters.AddWithValue("@linkid", user.linkid),
                cmd.Parameters.AddWithValue("@emailid", user.emailid),
                cmd.Parameters.AddWithValue("@username", user.username),
                cmd.Parameters.AddWithValue("@mobile", user.mobile),
                cmd.Parameters.AddWithValue("@password", user.password),
                cmd.Parameters.AddWithValue("@isactive", user.isactive),
                cmd.Parameters.AddWithValue("@isdelete", user.isdelete)
                //cmd.Parameters.AddWithValue("@dob", user.dob),
                //cmd.Parameters.AddWithValue("@createdon", user.createdon),
            };

                    cmd.Connection = con;
                    cmd.CommandText = _commonfunction.getInsertQuery(parameter, "UserMaster");
                    cmd.CommandType = CommandType.Text;

                    int status = cmd.ExecuteNonQuery();
                    if (status == 1)
                    {
                        result = _commonfunction.successResult(rstatus, "user inserted");
                    }
                    else
                    {
                        result = _commonfunction.errorResult();
                    }
                    return result;
                }
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

        public string getuserbyId(user user)
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                object[] param = new object[1];
                resultStatus rstatus = new resultStatus();
                dynamic result = "";
                if (user.emailid != "")
                {
                    param[0] = cmd.Parameters.AddWithValue("@emailid", user.emailid);
                }
                else
                {
                    param[0] = cmd.Parameters.AddWithValue("@mobile", user.mobile);
                }
                cmd.CommandText = _commonfunction.getSelectQuery("usermaster", param);
                cmd.Connection = con;
                con.Open();
                var status = cmd.ExecuteNonQuery();
                con.Close();
              
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = dt.AsEnumerable().Select(x =>
                    {
                        return x.Table;
                    }).FirstOrDefault();

                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    return _commonfunction.successResult(rstatus, "User does not exist");
                }
                

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string userLogin(user user)
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                dynamic result = "";
                resultStatus rstatus = new resultStatus();
                var param = new object[4];
                
                    param[0] = cmd.Parameters.AddWithValue("@emailid",user.emailid);
                    param[1] = cmd.Parameters.AddWithValue("@password",user.password);
                    param[2] = cmd.Parameters.AddWithValue("@isdelete",user.isdelete);
                    param[3] = cmd.Parameters.AddWithValue("@isactive", 1);

                cmd.Connection = con;

                var selectedColumn = new object[6];
                selectedColumn[0] = param[0];
                selectedColumn[1] = cmd.Parameters.AddWithValue("@username","");
                selectedColumn[2] = cmd.Parameters.AddWithValue("@linkid", 0);
                selectedColumn[3] = cmd.Parameters.AddWithValue("@mobile", 0);
                selectedColumn[4] = cmd.Parameters.AddWithValue("@dob", "");
                selectedColumn[5] = cmd.Parameters.AddWithValue("@createdon", "");

                cmd.CommandText = _commonfunction.getSelectQuery("usermaster", param, selectedColumn);
                con.Open();
                var status = cmd.ExecuteNonQuery();
                

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds,"login");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rstatus.Data = ds.Tables[0];
                    return _commonfunction.successResult(rstatus);
                }
                else
                {
                    return _commonfunction.errorResult("User does not exist");
                }
                
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public string sendOtp(user user)
        {
           
            otphits otpModel = new otphits();
            SqlCommand cmd = new SqlCommand();
            MailRequest mailRequest = new MailRequest();
            resultStatus rstatus = new resultStatus();


            Random generator = new Random();
            int otp = Convert.ToInt32(generator.Next(0, 1000000).ToString("D6"));
            dynamic result;
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {

                otpModel.emailid = user.emailid;
                otpModel.mobile = user.mobile;
                otpModel.otp = otp;
                otpModel.isdelete = 0;

                var param = new object[4];
                param[0] = cmd.Parameters.AddWithValue("@emailid", user.emailid);
                param[1] = cmd.Parameters.AddWithValue("@mobile", user.mobile);
                param[2] = cmd.Parameters.AddWithValue("@otp", otp);
                param[3] = cmd.Parameters.AddWithValue("@isdelete", user.isdelete);
            
                
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = _commonfunction.getInsertQuery(param, "otphits");
                cmd.CommandType = CommandType.Text;

                int status = cmd.ExecuteNonQuery();
                con.Close();
                if (status == 1)
                {
                    var emailtemplate = UtilFunction.otpTemplate(otpModel);

                    mailRequest.Subject = "YatraG Otp";
                    mailRequest.Body = emailtemplate;
                    mailRequest.ToEmail = user.emailid;
                    _IEmailSendRepo.SendEmailAsync(mailRequest);


                    result = _commonfunction.successResult(rstatus, "otp sent");
                }
                else
                {
                    result = _commonfunction.errorResult();
                }
                return result;

            }
            catch (Exception ex)
            {
                return _commonfunction.errorResult();
            }
        }

        public string verifyOtp(otphits otphits)
        {
            dynamic result = null;
            var con = new SqlConnection(_dbConnection.getConnectionString());
            resultStatus rstatus = new resultStatus();
            try
            {
                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand();
                object[] param = {
                  cmd.Parameters.AddWithValue("@emailid" , otphits.emailid),
                  cmd.Parameters.AddWithValue("@isdelete" , 0)
                  };
                
                cmd.CommandText = _commonfunction.getSelectQuery("otphits", param);
                cmd.Connection = con;
                
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Otp");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["otp"]) == otphits.otp)
                    {
                        var columnname = "isdelete";
                        cmd.Parameters.Clear();
                        var updateColumn = new object[1];
                        updateColumn[0] = cmd.Parameters.AddWithValue("@"+ columnname, 1);

                        var whereparam = new object[1];
                        whereparam[0] = cmd.Parameters.AddWithValue("@emailid", otphits.emailid);
                        cmd.CommandText = _commonfunction.getUpdateQuery(updateColumn, "otphits", whereparam);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        columnname = "isactive";
                        updateColumn[0] = cmd.Parameters.AddWithValue("@" + columnname, 1);
                        cmd.CommandText = _commonfunction.getUpdateQuery(updateColumn, "usermaster", whereparam);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        result = _commonfunction.successResult(rstatus, "Otp Verified");
                    }
                    else
                    {
                        result = _commonfunction.errorResult();
                    }
                }
                
                con.Close();
            }
            catch (Exception ex)
            {
                result= _commonfunction.errorResult();
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}
