using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "Deberias escribir un nombre.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
