namespace Ecommerce.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // ID del pedido al que pertenece este elemento
        public int ProductId { get; set; } // ID del producto
        public int Quantity { get; set; } // Cantidad del producto en el pedido
        public decimal Price { get; set; } // Precio del producto en el momento del pedido
    }
}
