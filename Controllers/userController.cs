using Microsoft.AspNetCore.Mvc;
using yatracub.Models;
using yatracub.Services;
using yatracub.Services.Interface;
using yatracub.Util;

namespace yatracub.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

    public class userController : BaseController
    {
        private readonly Iuserservice _userService;

        public userController(Iuserservice userservice)
        {
            _userService = userservice;
        }

        [HttpGet("getUser")]
        public string GetUser()
        {
            try
            {
               var result = _userService.GetUser();
                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("saveUpdateUser")]
        public IActionResult saveUpdateUser(user user)
        {
            try
            {
                var result = _userService.saveUpdateUser(user);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getUserById")]
        public IActionResult getuserbyId(user user)
        {
            try
            {
                var result = _userService.getuserbyId(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult userLogin(user user)
        {
            try
            {
                var result = _userService.userLogin(user);
                    return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("sendOtp")]
        public IActionResult sendOtp(user user)
        {
            try
            {
                var result = _userService.sendOtp(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("verifyOtp")]
        public IActionResult verifyOtp(otphits otphits)
        {
            try
            {
                var result = _userService.verifyOtp(otphits);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
