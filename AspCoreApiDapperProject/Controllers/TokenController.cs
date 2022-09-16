using AspCoreApiDapperProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Controllers
{
    [Route("api/[Controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if(user!=null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private string BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
              _config["Jwt:ValidAudience"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserModel Authenticate(LoginModel login)    
        {
            UserModel user = null;
            if(login.Username=="Einfochips" && login.Password=="Secret")
            {
                user = new UserModel { Name = "Seema Kushwaha", Email = "SeemaDevi.Kushwaha@einfochips.com" };
            }
            return user;
        }
        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
