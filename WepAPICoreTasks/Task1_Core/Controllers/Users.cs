using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1_Core.DTOs;
using Task1_Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly TokenGenerator _tokenGenerator;


        public Users(MyDbContext db, PasswordHasher<User> passwordHasher, TokenGenerator tokenGenerator)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;

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
        //[HttpPost]
        //public IActionResult CreateUser([FromForm] DTOsUser dto)
        //{
        //    var hashedPassword = _passwordHasher.HashPassword(new User(), dto.PasswordHash);

        //    var user = new User
        //    {
        //        Username = dto.Username,
        //        //PasswordHash = hashedPassword,
        //        Email = dto.Email
        //    };

        //    // Add and save the new user
        //    _db.Users.Add(user);
        //    _db.SaveChanges();

        //    return Ok(user);
        //}



        [HttpPost("register")]
        public ActionResult Register([FromForm] UserDTO model)
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

            _db.Users.AddAsync(user);
            _db.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] DTOsLogin model)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }
            var roles = _db.UserRoles.Where(x=>x.User.UserId == user.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);
            // Generate a token or return a success response
            return Ok(new { Token = token });
        }
        [HttpGet("/getname{name}")]
        public IActionResult GetDetails(string name)
        {
            var user = _db.Users.Where(x => x.Username == name).FirstOrDefault();
            return Ok(user);
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
