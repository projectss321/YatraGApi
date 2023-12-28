using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class stateCityService : IStateCityService
    {   
        private readonly IstateCityRepo _stateCityRepo;
        public stateCityService(IstateCityRepo stateCityRepo) {
            _stateCityRepo = stateCityRepo;
        }
        public string saveUpdateState(dynamic[] data)
        {
            if (1 == 2)
            {
                return _stateCityRepo.saveUpdateState(data);
            }
            else
            {
                return _stateCityRepo.saveUpdateCity(data); 
            }
        }

        public string getCityList()
        {
            return _stateCityRepo.getCityList();
        }

        public string getCityDetail()
        {
            return _stateCityRepo.getCityDetail();
        }
        public async Task<resultStatus> SaveUpdateCityDetail(List<IFormFile> fileList)
        {
            return await _stateCityRepo.SaveUpdateCityDetail(fileList);
        }

    }
}
