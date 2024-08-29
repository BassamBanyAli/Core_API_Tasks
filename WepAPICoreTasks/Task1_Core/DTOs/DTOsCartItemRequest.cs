using Task1_Core.Models;

namespace Task1_Core.DTOs
{
    public class DTOsCartItemRequest
    {

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }


    }
}
