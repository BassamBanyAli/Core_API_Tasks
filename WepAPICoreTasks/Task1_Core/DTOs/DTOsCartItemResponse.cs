using Task1_Core.Models;

namespace Task1_Core.DTOs
{
    public class DTOsCartItemResponse
    {

        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public DTOsProductResponse Product { get; set; }

    }
}
