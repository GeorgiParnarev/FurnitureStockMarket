namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Categories : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategories());
        }

        public IEnumerable<Category> CreateCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name="Kitchen"
                },
                new Category()
                {
                    Id=2,
                    Name="Livingroom"
                }
            };
        }
    }
}
