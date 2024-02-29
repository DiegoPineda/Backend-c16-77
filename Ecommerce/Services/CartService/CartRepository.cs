using Ecommerce.DbContexts;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CartService
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceContext _context;

        public CartRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task AddProductToCartAsync(Users user, int productId, int quantity)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                // Si el usuario no tiene un carrito, crear uno nuevo
                cart = new Cart { UserId = user.Id };
                _context.Cart.Add(cart);
            }

            // Buscar el producto en la base de datos
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                // Añadir el producto al carrito o actualizar la cantidad si ya está en el carrito
                var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
                }

                // Calcular el precio total del carrito
                cart.TotalPrice += product.Price * quantity;
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductFromCartAsync(Users user, int productId)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                // Si el usuario no tiene un carrito, no hay productos que eliminar
                return;
            }

            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);

                // Buscar el producto en la base de datos
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    // Restar el precio del producto eliminado del precio total del carrito
                    cart.TotalPrice -= product.Price * cartItem.Quantity;
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task ClearCartAsync(Users user)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                // Si el usuario no tiene un carrito, no hay productos que eliminar
                return;
            }

            // Eliminar todos los elementos del carrito
            cart.CartItems.Clear();

            // Reiniciar el precio total del carrito
            cart.TotalPrice = 0;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}
