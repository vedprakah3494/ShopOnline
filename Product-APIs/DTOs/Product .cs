using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Product_APIs.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}
