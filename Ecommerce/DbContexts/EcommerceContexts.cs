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
        public DbSet<Orders> Order { get; set; } = null!;
        public DbSet<OrderItem> OrderItem { get; set; } = null!;


        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar el tipo de columna SQL para la propiedad 'TotalPrice' en la entidad 'Cart'
            modelBuilder.Entity<Cart>()
                .Property(c => c.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            // Configurar el tipo de columna SQL para la propiedad 'Price' en la entidad 'Product'
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        }

    }
}

