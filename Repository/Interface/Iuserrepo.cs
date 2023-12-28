using Microsoft.AspNetCore.Mvc;
using yatracub.Models;

namespace yatracub.Repository.Interface
{
    public interface Iuserrepo
    {
        public string GetUser();
        public string saveUpdateUser(user user);
        public string getuserbyId(user user);
        public string userLogin(user user);
        public string sendOtp(user user);
        public string verifyOtp(otphits otphits);
    }
}
