using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OurVisitors.Models;

namespace OurVisitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly OurVisitorsContext _context;
        private readonly IConfiguration _config;

        public AuthController(OurVisitorsContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Users userLogin)
        {
            var user = _context.Users.FirstOrDefault((x) => ((x.Username == userLogin.Username || x.Email == userLogin.Username) && x.Password == userLogin.Password));
            // var user = _context.Users.Where(x => (x.Username == userLogin.Username || x.Email == userLogin.Email))
            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var libelle = await GetRole(user.IdRole);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                username = user.Username,
                role = libelle
            });
        }

        public async Task<string> GetRole(int? id)
        {
            var role = await _context.Role.FirstOrDefaultAsync(x => x.Id == id);
            if (role == null)
                return "null";
            return role.Libelle;
        }

    }
}