using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectLibrary.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly List<User> users;
        private IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            users = new List<User>
            {
                new User { Id = 0, Login = "log1", Password = "pass1" },
                new User { Id = 1, Login = "log2", Password = "pass2" },
                new User { Id = 2, Login = "log3", Password = "pass3" },
            };

            this.configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var identity = GetIdentity(user);

            if (identity == null)
            {
                return Unauthorized(user);
            }

            string encodedJwt = GetEncodedJwt(identity);

            return Ok(encodedJwt);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var existed = users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);

            if (existed != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, existed.Login),
                };

                return new ClaimsIdentity(claims, "Token");
            }

            return null;
        }

        private string GetEncodedJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"])),
                algorithm: SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(configuration["Jwt:LifeTimeInMinutes"]))),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
