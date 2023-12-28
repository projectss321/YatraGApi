using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using yatracub.Models;

namespace yatracub.Services.Interface
{
    public interface IStateCityService
    {
        public string saveUpdateState(dynamic[] data);
        public string getCityList();
        public string getCityDetail();
        public Task<resultStatus> SaveUpdateCityDetail(List<IFormFile> fileList);
    }
}
