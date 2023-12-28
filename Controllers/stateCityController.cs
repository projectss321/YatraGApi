using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Web.Http.Results;
using yatracub.Models;
using yatracub.Services.Interface;

namespace yatracub.Controllers
{
    public class stateCityController : BaseController
    {
        private readonly IStateCityService _stateCityService;
        public stateCityController(IStateCityService stateCityService)
        {
            _stateCityService = stateCityService;
        }
        [HttpGet("getCity")]
        public IActionResult getCityList()
        {
            try
            {
               var result = _stateCityService.getCityList();
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saveUpdateState")]
        public IActionResult saveUpdateState([FromBody] dynamic[] data)
        {
            try
            {
                var result = _stateCityService.saveUpdateState(data);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saveUpdateCity")]
        public IActionResult saveUpdateCity([FromBody] dynamic[] data)
        {
            try
            {
                var result = _stateCityService.saveUpdateState(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCityDetail")]
        public IActionResult getCityDetail()
        {
            try
            {
                var result = _stateCityService.getCityDetail();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        [HttpPost("SaveUpdateCityDetail"), DisableRequestSizeLimit]

        public async Task<resultStatus> SaveUpdateCityDetail([FromBody] dynamic[] fileList)
        {
            resultStatus rstatus = new resultStatus();
            //var con = new SqlConnection(_dbConnection.getConnectionString());
            var appDir = System.IO.Directory.GetCurrentDirectory();
            int index = appDir.IndexOf("Api\\", 0);
            appDir = "D:\\Project\\web\\src\\assets\\testingimg";
            try
            {
                dynamic array = new dynamic[fileList.Length];
                foreach (var file in fileList)
                {
                    array.Add(file);
                }
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                //cmd.Connection = con;

                if (fileList == null || fileList.Length == 0)
                {
                    rstatus.message = "No file Found";
                }
                else
                {
                    foreach (var item in fileList)
                    {
                        if (!Directory.Exists(appDir)) ;
                        {
                            Directory.CreateDirectory(appDir);
                        }

                        //var path = Path.Combine(appDir, item);
                        //var file = new Models.FileAttachment
                        //{
                        //    fileName = item.FileName,
                        //    filePath = path,
                        //    fileExtension = Path.GetExtension(item.FileName)
                        //};

                        //using (var stream = new FileStream(path, FileMode.Create))
                        //{
                            //await item.CopyToAsync(stream);
                        //}


                    }
                }

                return rstatus;
            }
            catch (Exception ex)
            {
                rstatus.message = ex.Message;
                return rstatus;
            }
            finally
            {
                //con.Close();
            }
        }



        //public async Task<resultStatus> SaveUpdateCityDetail(List<IFormFile> fileList)
        //{
        //    resultStatus rstatus = new resultStatus();
        //    try
        //    {

        //        rstatus = await _stateCityService.SaveUpdateCityDetail(fileList);
        //        return rstatus;
        //    }
        //    catch (Exception ex)
        //    {
        //        rstatus.message = ex.Message;
        //        return rstatus;
        //    }

        //}
    }
}
