using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DbContexts
{
    public class EcommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Brand> Brand { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Cart> Cart { get; set; } = null!;
        public DbSet<CartItem> CartItem { get; set; } = null!;


        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

    }
}

