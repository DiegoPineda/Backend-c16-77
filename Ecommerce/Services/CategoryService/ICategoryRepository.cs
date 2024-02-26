using Ecommerce.Entities;

namespace Ecommerce.Services.CategoryService
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
