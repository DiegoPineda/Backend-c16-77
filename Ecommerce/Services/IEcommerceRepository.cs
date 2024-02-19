using Ecommerce.Entities;

namespace Ecommerce.Services
{
    public interface IEcommerceRepository
    {
        IQueryable<Product> GetProducts();
        Task<Product> GetProductByIdAsync(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task SaveAsync();
    }
}
