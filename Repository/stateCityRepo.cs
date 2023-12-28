using Abp.Domain.Uow;
using Hangfire;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.Common;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web.Helpers;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Shared;
using yatracub.Shared.Interface;
using yatracub.Util;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace yatracub.Repository
{
    public class stateCityRepo : IstateCityRepo
    {
        private IdbContext _dbConnection;
        private readonly IcommonFunction _commonfunction;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public stateCityRepo(IdbContext dbContext, IcommonFunction commonFunction, IWebHostEnvironment hostingEnvironment)
        {
            _dbConnection = dbContext;
            _commonfunction = commonFunction;
            _hostingEnvironment = hostingEnvironment;
        }

        public string getCityList()
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                DataTable dt = new DataTable();
                resultStatus rstatus = new resultStatus();

                SqlCommand cmd = new SqlCommand("select * from citymaster nolock", con);
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
                rstatus.Data = result;
                return _commonfunction.successResult(rstatus);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string saveUpdateState(dynamic[] data)
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                var result = "";
                string serializedObject = System.Text.Json.JsonSerializer.Serialize(data);
                resultStatus rstatus = new resultStatus();

                var ja = (JArray)JsonConvert.DeserializeObject(serializedObject);
                var jo = (JObject)ja[0];
                long lastLinkid = 0;

                for (int i = 0; i < data.Length; i++)
                {

                    //if (Convert.ToInt64(ja[i]["linkid"]) != lastLinkid)
                    //{
                    long linkid = UtilFunction.GetLinkid();
                        SqlParameter[] param = {
                        cmd.Parameters.AddWithValue("@latitude", Convert.ToDouble(ja[i]["latitude"])),
                        cmd.Parameters.AddWithValue("@longitude", Convert.ToDouble(ja[i]["longitude"])),
                        cmd.Parameters.AddWithValue("@linkid", Convert.ToInt64(linkid)),
                        cmd.Parameters.AddWithValue("@name", ja[i]["name"].ToString()),
                        cmd.Parameters.AddWithValue("@state_code", ja[i]["state_code"].ToString()),
                        cmd.Parameters.AddWithValue("@type", ja[i]["type"].ToString()),
                        cmd.Parameters.AddWithValue("@isactive", 1),
                        cmd.Parameters.AddWithValue("@isdelete", 0)
                };
                        //lastLinkid = (long)param.Where(xx => xx.ParameterName == "@linkid").Select(yy => yy.Value).ToList()[0];
                        cmd.Connection = con;
                        cmd.CommandText = _commonfunction.getInsertQuery(param, "statemaster");
                        con.Open();
                        int status = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        if (status == 1)
                        {
                            result = _commonfunction.successResult(rstatus);
                            con.Close();
                        }
                        else
                        {
                            result = _commonfunction.errorResult();
                            con.Close();
                        }
                    //}
                    //else
                    //{
                    //    return "duplicate linkid found" + Convert.ToInt64(ja[i]["linkid"]) + "'" + "state name is = " + ja[i]["name"].ToString();
                    //}
                }
                //transaction.Commit();
                con.Close();
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
        public string saveUpdateCity(dynamic[] data)
        {
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                var result = "";
                string serializedObject = System.Text.Json.JsonSerializer.Serialize(data);
                resultStatus rstatus = new resultStatus();
                var ja = (JArray)JsonConvert.DeserializeObject(serializedObject);
                var jo = (JObject)ja[0];
                long lastLinkid = 0;

                for (int i = 0; i < data.Length; i++)
                {
                    //foreach (var item in data)
                    //{



                        //if (Convert.ToInt64(ja[i]["linkid"]) != lastLinkid)
                        //{
                            long linkid = UtilFunction.GetLinkid();
                            SqlParameter[] param = {
                        cmd.Parameters.AddWithValue("@latitude", Convert.ToDouble(ja[i]["latitude"])),
                        cmd.Parameters.AddWithValue("@longitude", Convert.ToDouble(ja[i]["longitude"])),
                        cmd.Parameters.AddWithValue("@linkid", Convert.ToInt64(linkid)),
                        cmd.Parameters.AddWithValue("@name", ja[i]["name"].ToString()),
                        cmd.Parameters.AddWithValue("@state_id", Convert.ToInt32(ja[i]["state_id"])),
                        cmd.Parameters.AddWithValue("@state_code", ja[i]["state_code"].ToString()),
                        cmd.Parameters.AddWithValue("@isactive", 1),
                        cmd.Parameters.AddWithValue("@isdelete", 0)
                };
                           // lastLinkid = (long)param.Where(xx => xx.ParameterName == "@linkid").Select(yy => yy.Value).ToList()[0];
                            cmd.Connection = con;
                            cmd.CommandText = _commonfunction.getInsertQuery(param, "citymaster");
                            con.Open();
                            int status = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            if (status == 1)
                            {
                                result = _commonfunction.successResult(rstatus);
                                con.Close();
                            }
                            else
                            {
                                result = _commonfunction.errorResult();
                                con.Close();
                            }
                        //}
                        //else
                        //{
                        //    return "duplicate linkid found" + Convert.ToInt64(ja[i]["linkid"]) + "'" + "state name is = " + ja[i]["name"].ToString();
                        //}
                    }
                //}
                    //transaction.Commit();
                    con.Close();
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

        public string getCityDetail()
        {
            resultStatus rstatus = new resultStatus();
            var con = new SqlConnection(_dbConnection.getConnectionString());
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select cd.linkid as citydetaillinkid,cd.citylinkid,cd.img,cd.img2,cd.img3,cd.img4,cd.img5,cd.img6,cd.img7,cd.img8,cd.img9,cd.img10,cd.img11,cd.img12,cd.img13,cd.header,cd.subheader,cd.subheader2,cd.subheader3,cd.content,cd.neareststationname,cd.stationdistance,cd.nearestbusname,cd.busdistance,cd.nearestairport,cd.airportdistance,cm.linkid as citymasterlinkid,cm.latitude,cm.longitude,cm.name,cm.state_id,cm.state_code from citydetail (nolock) cd inner join citymaster (nolock) cm on cd.citylinkid = cm.linkid where cm.isdelete=0 and cm.isactive=1";
                con.Open();

                var status = cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "citydetail");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rstatus.Data = ds.Tables[0];
                  return  _commonfunction.successResult(rstatus);
                }
                else
                {
                  return   _commonfunction.errorResult("citydetail not available");
                }
            }
            catch (Exception ex)
            {
                 return _commonfunction.errorResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            
        }

        public async Task<resultStatus> SaveUpdateCityDetail(List<IFormFile> fileList)
        {
            resultStatus rstatus = new resultStatus();
            var con = new SqlConnection(_dbConnection.getConnectionString());
            var appDir = System.IO.Directory.GetCurrentDirectory();
            int index = appDir.IndexOf("Api\\", 0);
            appDir = "D:\\Project\\web\\src\\assets\\testingimg";


            try
            {
                dynamic array = new dynamic[fileList.Count];
                foreach (var file in fileList)
                {
                    array.Add(file);
                }
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                    
                if(fileList == null || fileList.Count == 0)
                {
                   rstatus.message = "No file Found";
                }
                else
                {
                    foreach (var item in fileList)
                    {
                        if (!Directory.Exists(appDir));
                        {
                            Directory.CreateDirectory(appDir);
                        }

                        var path = Path.Combine(appDir, item.FileName);
                        var file = new Models.FileAttachment
                        {
                            fileName = item.FileName,
                            filePath = path,
                            fileExtension = Path.GetExtension(item.FileName)
                        };
                        
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            //await item.CopyToAsync(stream);
                        }

                        
                    }
                }

                return rstatus;
            }catch (Exception ex)
            {
                rstatus.message = ex.Message;
                return rstatus;
            }
            finally
            {
                con.Close();
            }
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            return this._hostingEnvironment.WebRootPath + "\\files\\" + filename;
        }
    }
}
