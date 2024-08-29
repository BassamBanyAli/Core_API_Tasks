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
        public IActionResult getCartItems() {

            var items=_db.CartItems.Select(x => new DTOsCartItemResponse
            {
                CartItemId = x.CartItemId,
                CartId = x.CartId,
                Quantity = x.Quantity,
                Product=new DTOsProductResponse
                {
                    ProductId=x.Product.ProductId,
                    ProductName=x.Product.ProductName,
                    Price=x.Product.Price,
                }



            });


            if(items!=null)
                return Ok(items);
            return NoContent();
        }
        [HttpPost]
        public IActionResult postCartItems([FromBody] DTOsCartItemRequest item) {
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
    }
}
