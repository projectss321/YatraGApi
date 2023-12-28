using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using yatracub.Repository.Interface;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class userservice : Iuserservice
    {
        private readonly Iuserrepo _iuserrepo;
        public userservice(Iuserrepo iuserrepo)
        {
            _iuserrepo = iuserrepo;
        }

        public string GetUser()
        {
         return _iuserrepo.GetUser();
        }

        public string saveUpdateUser(user user)
        {
           return _iuserrepo.saveUpdateUser(user);
        }

       public string getuserbyId(user user)
        {
            return _iuserrepo.getuserbyId(user);
        }

        public string userLogin(user user)
        {
            return _iuserrepo.userLogin(user);
        }

        public string sendOtp(user user)
        {
            return _iuserrepo.sendOtp(user);
        }

        public string verifyOtp(otphits otphits)
        {
            return _iuserrepo.verifyOtp(otphits);
        }
    }
}
