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

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Cart
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Cart.AddAsync(cart);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
