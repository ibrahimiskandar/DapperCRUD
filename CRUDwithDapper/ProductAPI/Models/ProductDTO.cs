using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class ProductDTO
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        public int ProductStock { get; set; }
    }
}
