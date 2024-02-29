using Ecommerce.DbContexts;
using Ecommerce.Entities;
using Ecommerce.Services.CartService;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Orderservice
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceContext _context;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(EcommerceContext context, ICartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }
        public async Task<Orders> CreateOrderFromCartAsync(Users user)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null || cart.CartItems.Count == 0)
            {
                return null; // No se puede crear un pedido vacío
            }

            // Crear un nuevo pedido basado en el carrito actual
            var order = new Orders
            {
                UserId = user.Id,
                OrderDate = DateTime.Now, // Puedes ajustar la fecha del pedido según sea necesario
                TotalPrice = cart.TotalPrice,
                OrderItems = cart.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price // Utiliza el precio del producto en el momento de la creación del pedido
                }).ToList()
            };

            // Agregar el nuevo pedido a la base de datos
            _context.Order.Add(order);

            // Eliminar el carrito (opcional, depende de tu lógica de negocio)
            await _cartRepository.ClearCartAsync(user);


            await _context.SaveChangesAsync();

            return order;
        }
    }
}
