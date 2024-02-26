using Ecommerce.DbContexts;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.BrandService
{
    public class BrandRepository : IBrandRepository
    {
        private readonly EcommerceContext _context;

        public BrandRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brand.ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _context.Brand.FindAsync(id);
        }

        public async Task AddBrandAsync(Brand brand)
        {
            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
