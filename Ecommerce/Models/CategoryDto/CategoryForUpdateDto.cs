using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.CategoryDto
{
    public class CategoryForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
