using Ecommerce.Entities;

namespace Ecommerce.Services.BrandService
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> GetBrandByIdAsync(int id);
        Task AddBrandAsync(Brand brand);
        Task UpdateBrandAsync(Brand brand);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

    }
}
