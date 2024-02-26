using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
