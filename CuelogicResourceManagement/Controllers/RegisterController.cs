
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MySqlX.XDevAPI.Common;
using Services.Contracts;

namespace CuelogicResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRegisterServices _registerServices;


        public RegisterController(IConfiguration configuration, IRegisterServices registerServices)
        {
            _configuration = configuration;
            _registerServices = registerServices;
        }



     
        [HttpPost]
        [Route("register")]
       
        public async Task<IActionResult> Register(Registration user)
        {
            var res = await _registerServices.RegisterNewUser(user);
            if (res == true)
            {
                return Ok(
                    new ApiResponse<Employee>
                    {
                        Success = true,
                        Message = Messages.UserRegistrationSuccess
                    });
            }
            return BadRequest(new ApiResponse<Employee>
            {
                Success = true,
                Message = Messages.UserRegistrationFail
            });

        }




        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, string password, string confirmPassword, string newPassword)
        {

            var res = await _registerServices.ChangeUserPassword(id, password, confirmPassword, newPassword);
            if (res == true)
            {
                return Ok(new ApiResponse<Employee>
                {
                    Success = true,
                    Message = "Password changed"
                });
            }
            return BadRequest(new ApiResponse<Employee>
            {
                Success = true,
                Message = "Failed to change Password"
            });

        }

    }
}

