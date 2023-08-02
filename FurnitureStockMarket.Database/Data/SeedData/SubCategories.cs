namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SubCategories : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(CreateSubCategories());
        }

        public IEnumerable<SubCategory> CreateSubCategories()
        {
            return new List<SubCategory>()
            {
                new SubCategory()
                {
                    Id = 1,
                    Name="Tables",
                    CategoryId = 1,
                },
                new SubCategory()
                {
                    Id = 2,
                    Name="TV",
                    CategoryId = 2,
                },
                new SubCategory()
                {
                    Id = 3,
                    Name="Chair",
                    CategoryId = 2,
                }
            };
        }
    }
}
