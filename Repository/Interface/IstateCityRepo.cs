using yatracub.Models;

namespace yatracub.Repository.Interface
{
    public interface IstateCityRepo
    {
        public string saveUpdateState(dynamic[] data);
        public string saveUpdateCity(dynamic[] data);
        public string getCityList();
        public string getCityDetail();
        public Task<resultStatus> SaveUpdateCityDetail(List<IFormFile> fileList);
    }
}
