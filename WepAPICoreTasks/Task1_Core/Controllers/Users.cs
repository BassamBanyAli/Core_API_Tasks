using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Core.DTOs;
using Task1_Core.Models;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly MyDbContext _db;

        public Users (MyDbContext db)
        {

        _db = db; 
        
        }
        [HttpGet]
        public IActionResult getAllUsers()
        {
            var Users = _db.Users.ToList();
            if (Users.Any())
                return Ok(Users);
            return NoContent();
        }
        [HttpPost]
        public IActionResult CreateProduct([FromForm] DTOsUser dto)
        {
            //Save the Image on the server
            

            // Create new category
            var User = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email
            };

            //Add and save the new catagery
            _db.Users.Add(User);
            _db.SaveChanges();

            return Ok(User);
        }
        [HttpPut("GetCategoryById/{id}")]
        public IActionResult UpdateUserById([FromForm] DTOsUser dto, int id)
        {
            var oldUser = _db.Users.Find(id);
            if (oldUser == null) return BadRequest();


            oldUser.Username = dto.Username;
            oldUser.Password = dto.Password;
            oldUser.Email = dto.Email;

            _db.SaveChanges();
            return Ok(oldUser);
        }
        [HttpGet("GetUserByName/{Name}")]
        public IActionResult GetUserByName(string Name)
        {


            if (string.IsNullOrWhiteSpace(Name))
            {
                return BadRequest("Name parameter is required.");
            }
            var User = _db.Users.Where(c => c.Username == Name).FirstOrDefault();
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }


        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {



            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }
            var User = _db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {



            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }

            var User = _db.Users.Find(id);

            if (User == null)
            {
                return NotFound();
            }

            _db.Users.Remove(User);
            _db.SaveChanges();
            return NoContent(); // Return the deleted category or a success message
        }


    }
}
