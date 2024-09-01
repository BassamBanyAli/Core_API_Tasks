using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Task1_Core.DTOs;
using Task1_Core.Models;
using  static Task1_Core.SaveImage.SaveImage;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly MyDbContext _db;
        public Products(MyDbContext db)
        {

            _db = db;
        }
        [HttpGet]
        public IActionResult getAllProducts()
        {
            var products = _db.Products.Include(p => p.Category).ToList();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("get")]
        public IActionResult getProducts()
        {
            var products = _db.Products.OrderByDescending(x=>x.ProductName).Take(5).ToList();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost]
        public IActionResult CreateProduct([FromForm] DTOsProduct dto)
        {
            //Save the Image on the server
            SaveImage1(dto.ProductImage);

            // Create new category
            var Product = new Product
            {
                ProductName = dto.ProductName,
                Description=dto.Description,
                Price=dto.Price,
                CategoryId=dto.CategoryId,
                ProductImage = dto.ProductImage.FileName
            };

            //Add and save the new catagery
            _db.Products.Add(Product);
            _db.SaveChanges();

            return Ok(Product);
        }
        [HttpPut("GetCategoryById/{id}")]
        public IActionResult UpdateCategoryById([FromForm] DTOsProduct dto, int id)
        {
            var oldCategory = _db.Categories.Find(id);
            if (oldCategory == null) return BadRequest();

            SaveImage1(dto.ProductImage);

            oldCategory.CategoryName = dto.ProductName;
            oldCategory.CategoryImage = dto.ProductImage.FileName;

            _db.SaveChanges();
            return Ok(oldCategory);
        }


        [HttpGet("GetProductByID")]
        public IActionResult GetProductByID([FromQuery] int? id)
        {


            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }
            var product = _db.Products.Where(p => p.ProductId == id)
      .FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("GetProductByCategoryID")]
        public IActionResult GetProductByCategoryID( [FromQuery] int?id)
        {


            if (id <= 0)
            {
                return BadRequest("ID parameter is required.");
            }
            var product = _db.Products.Where(p => p.CategoryId == id).OrderByDescending(p => p.Price)
      .ToList();
  
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("GetProductByName")]
        public IActionResult GetProductByName([FromQuery] string? Name)
        {
            {

                if (string.IsNullOrWhiteSpace(Name))
                {
                    return BadRequest("Name parameter is required.");
                }
                var product = _db.Products
          .Include(p => p.Category).Where(p => p.ProductName == Name)
          .FirstOrDefault();
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);

            }
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteItem( [FromQuery] int? id)
        {


            if (id <= 0)
            {
        return BadRequest("ID parameter is required.");
    }

            var Product = _db.Products.Find(id);

            if (Product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(Product);
            _db.SaveChanges();
            return NoContent();
        }


   

    [HttpGet("{id1}/{Price}")]
        public IActionResult GetCatPrice(int id1,int Price)
        {
            var products = _db.Products.Where(c=>c.CategoryId==id1&&c.Price>100).Count();
            return Ok(products);
        }

    }

}
    

