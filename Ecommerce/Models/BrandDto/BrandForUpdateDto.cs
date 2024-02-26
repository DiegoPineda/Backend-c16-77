using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.BrandDto
{
    public class BrandForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
