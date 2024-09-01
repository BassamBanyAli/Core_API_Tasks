using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1_Core.DTOs;
using Task1_Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly PasswordHasher<User> _passwordHasher;

        public Users(MyDbContext db, PasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;

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
        public IActionResult CreateUser([FromForm] DTOsUser dto)
        {
            var hashedPassword = _passwordHasher.HashPassword(new User(), dto.PasswordHash);

            var user = new User
            {
                Username = dto.Username,
                //PasswordHash = hashedPassword,
                Email = dto.Email
            };

            // Add and save the new user
            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(user);
        }



        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO model)
        {
            // Hash the password
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Username = model.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = model.Email
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(UserDTO model)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == model.UserName && x.Email == model.Email);
            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }
            // Generate a token or return a success response
            return Ok("User logged in successfully");
        }


















        [HttpPut("UpdateUserById/{id}")]
        public IActionResult UpdateUserById([FromForm] DTOsUser dto, int id)
        {
            var oldUser = _db.Users.Find(id);
            if (oldUser == null) return BadRequest();


            oldUser.Username = dto.Username;
            //oldUser.PasswordHash = dto.PasswordHash;
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
