using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Propiedad de navegación para el usuario propietario del carrito
        [Required]
        public int UserId { get; set; }

        // Lista de productos en el carrito
        [Required]
        public virtual ICollection<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}