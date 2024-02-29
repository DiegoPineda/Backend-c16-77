using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities
{
    public class Cart
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Propiedad de navegación hacia el usuario
        public Users User { get; set; }

        // Colección de elementos en el carrito
        public List<CartItem> CartItems { get; set; }

        // Precio total del carrito
        [Required]
        public decimal TotalPrice { get; set; }
    }
}