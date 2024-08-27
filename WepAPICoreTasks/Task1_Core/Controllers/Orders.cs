using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1_Core.Models;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders : ControllerBase
    {
        private readonly MyDbContext _db;

        public Orders(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult getAllOrders()
        {
            var Orders = _db.Orders.Include(p => p.OrderDate).ToList();
            if (Orders == null)
            {
                return NoContent();
            }

            return Ok(Orders);
        }
        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById([FromQuery] int id)
        {
            if (id<=0)
    {
        return BadRequest("ID parameter is required.");
    }
            var Order = _db.Orders
      .Include(p => p.User).Where(p => p.OrderId == id)
      .FirstOrDefault();
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }
        [HttpGet("GetOrderByName")]
        public IActionResult GetOrdereByName([FromQuery] string? Name)
        {
            {

                if (string.IsNullOrWhiteSpace(Name))
                {
                    return BadRequest("Name parameter is required.");
                }
                var Order = _db.Orders
          .Include(p => p.User).Where(p => p.User.Username == Name)
          .FirstOrDefault();
                   if (Order == null)
            {
                return NotFound();
            }
                return Ok(Order);

            }
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteItem([FromQuery] int id)
        {
            if (id <= 0)
            {
        return BadRequest("ID parameter is required.");
    }

            var Order = _db.Orders.Find(id);

            if (Order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(Order);
            _db.SaveChanges();
            return NoContent();
        }




     

    }

}


