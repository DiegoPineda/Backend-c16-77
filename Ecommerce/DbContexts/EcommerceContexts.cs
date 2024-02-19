using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DbContexts
{
    public class EcommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;


        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

    }
}

