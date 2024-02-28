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
        public Users User { get; set; }

        // Lista de productos en el carrito
        [Required]
        public virtual ICollection<CartItem> Items { get; set; }

        // Precio total del carrito
        [NotMapped] // Este atributo no se mapeará a la base de datos
        public decimal TotalPrice => CalculateTotalPrice();

        private decimal CalculateTotalPrice()
        {
            if (Items == null)
                return 0;

            return Items.Sum(item => item.Product.Price * item.Quantity);
        }
    }
}