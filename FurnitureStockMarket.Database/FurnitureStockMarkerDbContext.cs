namespace FurnitureStockMarker.Database
{
    using FurnitureStockMarker.Database.Models;
    using FurnitureStockMarker.Database.Models.Account;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FurnitureStockMarkerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public FurnitureStockMarkerDbContext(DbContextOptions<FurnitureStockMarkerDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
    }
}
