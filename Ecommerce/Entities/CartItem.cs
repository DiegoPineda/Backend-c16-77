using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        // Propiedad de navegación para el carrito al que pertenece este ítem
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
