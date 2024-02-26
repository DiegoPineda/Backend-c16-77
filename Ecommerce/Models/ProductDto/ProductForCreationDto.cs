using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ProductDto
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "Deberias escribir un nombre.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
