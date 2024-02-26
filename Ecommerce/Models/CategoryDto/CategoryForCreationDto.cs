using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.CategoryDto
{
    public class CategoryForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
