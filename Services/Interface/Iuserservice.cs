using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using static System.Net.WebRequestMethods;

namespace yatracub.Services.Interface
{
    public interface Iuserservice
    {
        public string GetUser();
        public string saveUpdateUser(user user);
        public string getuserbyId(user user);
        public string userLogin(user user);
        public string sendOtp(user user);
        public string verifyOtp(otphits otphits);
    }
}
