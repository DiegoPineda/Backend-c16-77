using Ecommerce.Entities;

namespace Ecommerce.Services.CartService
{
    public interface ICartRepository
    {
        Task AddProductToCartAsync(Users user, int productId, int quantity);
        Task AddCartAsync(Cart cart);
        Task SaveAsync();
        Task RemoveProductFromCartAsync(Users user, int productId);
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task ClearCartAsync(Users user);
    }
}
