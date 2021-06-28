using Microsoft.AspNetCore.Mvc;
using AngularProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly ConnectionDbClass _context;

        public registerController(ConnectionDbClass context)
        {
            _context = context;
        }

        // GET: api/<registerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<registerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        private ActionResult<Utilisateur> Utilisateur(Utilisateur a)
        {
            throw new NotImplementedException();
        }

        // POST api/<registerController>
        [HttpPost]
        public IActionResult Post([FromBody] Utilisateur user)
        {
            if(user.Email == "" || user.Username == "" && user.Password == "")
            {
                return Ok(new Response() { Status = "Error", Message = "Invalid User input" });
            }
            Utilisateur newUser = new Utilisateur();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            
            newUser.Username = user.Username;
            newUser.Role = "User";

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(new Response() { Status = "Success", Message = "Profile created successfuly" });

        }

        // PUT api/<registerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<registerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
