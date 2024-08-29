using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Core.Models;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cart : ControllerBase
    {
        private readonly MyDbContext _db;
        public Cart(MyDbContext db)
        {

            _db = db;
        }
        [HttpGet]
        public IActionResult getAllCart()
        {
            var cart = _db.Carts.ToList();
            if (cart.Any())
                return Ok(cart);
            return NoContent();
        }
    }
}
