using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Product(string name, string description, decimal price, string imageUrl, int stock, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Stock = stock;
            CategoryId = categoryId;
        }
    }
}

