using Ecommerce.Entities;

namespace Ecommerce.Services.Orderservice
{
    public interface IOrderRepository
    {
        Task<Orders> CreateOrderFromCartAsync(Users user);
    }
}
