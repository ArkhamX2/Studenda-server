using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Common;
using Studenda.Core.Server.utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Studenda.Core.Server.Controllers
{

    public class HomeController : Controller
    {
        private readonly DataContext context;
        new List<Person>  people = new List<Person>
        {
            new Person("tom@gmail.com", "12345"),
            new Person("bob@gmail.com", "55555")
        };

        public HomeController(DataContext _context)
        {
            context = _context;
        }

        [HttpGet("user/{id}")]
        public IActionResult GetId(int id)
        {
            return Json(context.Courses);
        }
        [HttpPost("login/{name}")]
        public IActionResult Login([FromBody]Person loginData)
        {
            Person? person = people.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null) return (IActionResult)Results.Unauthorized();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encode = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encode,
                username = person.Email
            };

            return Json(response);
        }
        [HttpPost("register")]
        public IActionResult Register() 
        { 


        }
        [Authorize]
        [HttpGet("Hello")]
        public IActionResult Hello()
        {
            return Json("Hello");
        }

    }
}
