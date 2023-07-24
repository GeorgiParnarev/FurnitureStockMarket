namespace FurnitureStockMarket.Database
{
    using Models;
    using Models.Account;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FurnitureStockMarketDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public FurnitureStockMarketDbContext(DbContextOptions<FurnitureStockMarketDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ProductsOrders>(entity =>
            //{
            //    entity.HasKey(x => new { x.OrderId, x.ProductId });
            //});

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        //public DbSet<ProductsOrders> ProductsOrders { get; set; } = null!;
    }
}
