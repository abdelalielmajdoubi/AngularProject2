using AngularProject2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularProject2.Controllers
{
    
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ConnectionDbClass _context;
        private readonly IConfiguration _configuration;

        public LoginController(ConnectionDbClass context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/<connectController>
        [HttpGet]
        [Authorize]
        [Route("api/login/Getverify")]
        public IActionResult GetLogin()
        {
            var idUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var utilisateur = _context.Users.Where(x => x.Id == int.Parse(idUser.Value));

            if (idUser == null)
            {
                return Ok(new Response() { Status = "Error", Message = "Utilisateur non verifier" });
            }
            return Ok(new { user = utilisateur });
        }

        [HttpGet("{id}")]
        [Route("api/login/Getelement/{id}")]
        public async Task<ActionResult<Utilisateur>> Getelement(long id)
        {
            var a = await _context.Users.FindAsync(id);

            if (a == null)
            {
                return NotFound();
            }

            return Utl(a);
        }

        private static Utilisateur Utl(Utilisateur Utl) =>
    new Utilisateur
    {
        Id = Utl.Id,
        Username = Utl.Username,
        Password = Utl.Password,
        Role = Utl.Role
    };

        // POST api/<connectController>
        [HttpPost]
        [Route("api/login/Postlogin/{user}")]
        public IActionResult Postlogin([FromBody] Utilisateur user)
        {
            var jwtToken = generateJsonWebToken(user);
            if (jwtToken == null)
            {
                return Ok(new Response() { Status = "Error", Message = "Username doas not exist" });
            }

            return Ok(new { token = jwtToken });
        }


        private string generateJsonWebToken(Utilisateur userInfo)
        {
            var user = _context.Users.Where(x => x.Username == userInfo.Username && x.Password == userInfo.Password).SingleOrDefault();
            if (user == null)
            {
                return null;
            }
            //var signingKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var expryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expryDuration),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("roles", user.Role),
                    new Claim("usernames", user.Username.ToString()),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
