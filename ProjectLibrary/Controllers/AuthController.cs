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
using ProjectLibrary.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ProjectLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<User> repository;
        private readonly IPasswordHasher<AuthInfo> hasher;

        public AuthController(IConfiguration configuration, IRepository<User> repository, IPasswordHasher<AuthInfo> hasher)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.hasher = hasher;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthInfo auth)
        {
            if (LoginAlreadyExists(auth))
            {
                return Conflict();
            }

            auth.Password = hasher.HashPassword(auth, auth.Password);

            var newUser = repository.Create(
                new User 
                { 
                    Auth = auth, 
                    Books = new List<Book>()
                });

            if (newUser == null)
            {
                return Conflict();
            }

            var identity = GetIdentity(auth);
            string encodedJwt = GetEncodedJwt(identity);

            return Ok(encodedJwt);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthInfo auth)
        {
            if (IsUserRegistered(auth) == false)
            {
                return Unauthorized(auth);
            }

            var identity = GetIdentity(auth);
            string encodedJwt = GetEncodedJwt(identity);

            return Ok(encodedJwt);
        }

        private bool LoginAlreadyExists(AuthInfo auth)
        {
            var existedUser = repository
                .GetAll()
                .FirstOrDefault(user => user.Auth.Login == auth.Login);

            return existedUser != null;
        }

        private bool IsUserRegistered(AuthInfo auth)
        {
            var users = repository.GetAll();

            foreach (var user in users)
            {
                if (user.Auth.Login == auth.Login)
                {
                    var result = hasher.VerifyHashedPassword(
                        user: auth, 
                        hashedPassword: user.Auth.Password, 
                        providedPassword: auth.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private ClaimsIdentity GetIdentity(AuthInfo auth)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, auth.Login),
            };

            return new ClaimsIdentity(claims, "Token");
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
