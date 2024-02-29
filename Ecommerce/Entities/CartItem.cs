using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // Id del producto
        [Required]
        public int ProductId { get; set; }

        // Cantidad del producto en el carrito
        [Required]
        public int Quantity { get; set; }

        // Propiedad de navegación hacia el producto
        [Required]
        public Product Product { get; set; }
    }
}
