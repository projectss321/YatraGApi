using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class packageService : Ipackageservice
    {
        private IpackageRepo _packageRepo;
        public packageService(IpackageRepo packageRepo)
        {
            _packageRepo = packageRepo;
        }
        public string getPackage()
        {
            return _packageRepo.getPackage();
        }
    }
}
