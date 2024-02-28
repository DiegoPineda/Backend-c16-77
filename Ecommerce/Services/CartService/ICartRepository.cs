using Ecommerce.Entities;

namespace Ecommerce.Services.CartService
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task UpdateCartAsync(Cart cart);
        Task AddCartAsync(Cart cart);
        Task SaveAsync();
    }
}
