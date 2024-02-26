using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.BrandDto
{
    public class BrandForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
