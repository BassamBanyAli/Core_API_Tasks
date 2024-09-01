using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Task1_Core.DTOs;
using Task1_Core.Models;

namespace Task1_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItems : ControllerBase
    {
        private readonly MyDbContext _db;
        public CartItems(MyDbContext db)
        {

            _db = db;
        }
        [HttpGet]
        public IActionResult getCartItems()
        {

            var items = _db.CartItems.Select(x => new DTOsCartItemResponse
            {
                CartItemId = x.CartItemId,
                CartId = x.CartId,
                Quantity = x.Quantity,
                Product = new DTOsProductResponse
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price,
                }



            });


            if (items != null)
                return Ok(items);
            return NoContent();
        }
        [HttpPost]
        public IActionResult postCartItems([FromBody] DTOsCartItemRequest item)
        {
            var items = new CartItem
            {
                CartId = item.CartId,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };



            _db.CartItems.Add(items);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPut("/GetTheProductId/{ProductID}")]
        public IActionResult UpdateCartItem([FromBody] DTOsCartItemRequestUpdate item, int ProductID)
        {


            var oldUpdate = _db.CartItems.Where(x => x.ProductId == ProductID).FirstOrDefault();
            if (oldUpdate == null) return BadRequest();

            oldUpdate.CartId = item.CartId;
            oldUpdate.Quantity = item.Quantity;
            _db.SaveChanges();

            return Ok(item);
        }
        [HttpDelete("/GetProductId/{ProductID}")]
        public IActionResult DeleteElement(int ProductID)
        {
            var item = _db.CartItems.Where(x => x.ProductId == ProductID).FirstOrDefault();
            if (item == null) return BadRequest();
            _db.CartItems.Remove(item);
            _db.SaveChanges();
            return Ok(item);
        }
    }
}