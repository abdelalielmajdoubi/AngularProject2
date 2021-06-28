using AngularProject2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gestionController : ControllerBase
    {

        private readonly ConnectionDbClass _context;
        private readonly IConfiguration _configuration;

        public gestionController(ConnectionDbClass context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/<gestionController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var idUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var role = _context.Users.Where(x => x.Id == int.Parse(idUser.Value)).Select(p => p.Role).SingleOrDefault();

            if (role == "User")
            {
                return Ok(new Response() { Status = "Error", Message = "Not Authorized" });
            }
            var users = _context.Users.Where(usr => usr.Role == "User").ToList();

            return Ok(new { usrs = users });
        }

        // GET api/<gestionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<gestionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<gestionController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleUtlisateur userInfo)
        {
            var idUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var role = _context.Users.Where(x => x.Id == int.Parse(idUser.Value)).Select(p => p.Role).SingleOrDefault();

            if (role == "User")
            {
                return Unauthorized(new Response() { Status = "Error", Message = "Not Authorized" });
            }

            Utilisateur user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Username = userInfo.Username;
            _context.SaveChanges();
            return Ok(new Response() { Status="success", Message = "User Updated successfuly" });
        }

        // DELETE api/<gestionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var idUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var role = _context.Users.Where(x => x.Id == int.Parse(idUser.Value)).Select(p => p.Role).SingleOrDefault();

            if (role == "User")
            {
                return Unauthorized(new Response() { Status = "Error", Message = "Not Authorized" });
            }

            Utilisateur user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(new Response() { Status = "success", Message = "User Deleted successfuly" });
        }
    }
}
