using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>()
        {
            new User {
                Firstname = "John",
                Lastname = "Smith",
                Age = 24
            },
            new User {
                Firstname = "Peter",
                Lastname = "Wolf",
                Age = 16
            },
            new User {
                Firstname = "Ellie",
                Lastname = "Portland",
                Age = 28
            },
            new User {
                Firstname = "Alicia",
                Lastname = "White",
                Age = 18
            }
        };

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                var user = users[id - 1];
                return Ok(user.Age >= 18 ? $"User with id: {id} found and is an adult" : $"User with id: {id} found and is NOT an adult");
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("No user with selected ID!");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            users.Add(user);
            return Ok("User saved!");
        }

    }
}
