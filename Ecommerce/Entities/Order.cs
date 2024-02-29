namespace Ecommerce.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // ID del usuario que realizó el pedido
        public DateTime OrderDate { get; set; } // Fecha del pedido
        public decimal TotalPrice { get; set; } // Precio total del pedido
        public List<OrderItem> OrderItems { get; set; } // Colección de elementos de pedido
    }
}
