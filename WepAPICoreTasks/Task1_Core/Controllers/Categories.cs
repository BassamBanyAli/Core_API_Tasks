using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Core.DTOs;
using Task1_Core.Models;
using static Task1_Core.SaveImage.SaveImage;

namespace Task1_Core.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly MyDbContext _db;

        public Categories(MyDbContext db)
        {

            _db = db;
        }
        [HttpGet]
        public IActionResult getAllCategories()
        {
            var categories = _db.Categories.ToList();
            if (categories.Any())
                return Ok(categories);
            return NoContent();
        }

        [HttpGet("Calc/{eq}")]
        public IActionResult getResult(string eq)
        {

            var op=eq.Split('+');
            return Ok(Convert.ToDecimal(op[0])+Convert.ToDecimal(op[1]));


        }
        [HttpPost]
        public IActionResult CreateCategory([FromForm] DTOsCategory dto)
        {
            //Save the Image on the server
            var b=SaveImage1(dto.CategoryImage);

            // Create new category
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CategoryImage = dto.CategoryImage.FileName,
            };

            //Add and save the new catagery
            _db.Categories.Add(category);
            _db.SaveChanges();

            return Ok(category);
        }
        [HttpPut("GetCategoryById/{id}")]
        public IActionResult UpdateCategoryById([FromForm] DTOsCategory dto, int id)
        {
            var oldCategory = _db.Categories.Find(id);
            if (oldCategory == null) return BadRequest();

           var b= SaveImage1(dto.CategoryImage);

            oldCategory.CategoryName = dto.CategoryName;
            oldCategory.CategoryImage = b;

            _db.SaveChanges();
            return Ok(oldCategory);
        }

    

        [HttpGet("GetCategoryByName/{Name}")]
        public IActionResult GetCategoryByName(string Name)
        {

            if (string.IsNullOrWhiteSpace(Name))
            {
                return BadRequest("Name parameter is required.");
            }
            var category = _db.Categories.Where(c => c.CategoryName == Name).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {


            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet("{number1}/{number2}")]
        public IActionResult sum(int number1, int number2)
        {
            if (number1 == 30 || number2 == 30)
            {
                return Ok(true);
            }
            if (number2 + number2 == 30)
            {
                return Ok(true);

            }
            return Ok(false);
        }
        [HttpGet("{number1}")]
        public IActionResult factorila(int number1)
        {
            if (number1>0&&(number1%3==0||number1%7==0))
            {
                return Ok(true);
            }
            
            return Ok(false);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {



            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent(); // Return the deleted category or a success message
        }


    }
}
