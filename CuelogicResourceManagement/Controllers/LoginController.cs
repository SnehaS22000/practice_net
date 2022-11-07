
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace CuelogicResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Manager")]
    public class LoginController : ControllerBase
    {

        private IConfiguration _configuration;
        private IUserServices _userServices;
        public LoginController(IConfiguration configuration, IUserServices userServices)
        {
            _userServices = userServices;

            _configuration = configuration;
        }

        [HttpPost]
        [Route("userlogin")]
        public async Task<IActionResult> UserLogin(Login user)
        {

            if (user != null)
            {

                var userData = await _userServices.GetUserDetail(user);
                string passwordHash = PasswordHashHelper.HashPassword(user.Password, userData.Salt);

                if (userData != null && passwordHash == userData.Password)
                {

                    var jwt = _configuration.GetSection("jwt").Get<Jwt>();
                    if (userData != null && userData.Role != null)
                    {
                        var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("UserName",user.Email),
                        new Claim(ClaimTypes.Role,userData.Role.ToString())
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                           jwt.Issuer,
                           jwt.Audience,
                            claims,
                            expires: DateTime.Now.AddMinutes(20),
                            signingCredentials: signIn
                        );
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    return Unauthorized("inavlid role");
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("something went wrong");
            }

        }
    }

}


