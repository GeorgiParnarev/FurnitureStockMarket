namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Reviews : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(CreateReviews());
        }

        public IEnumerable<Review> CreateReviews()
        {
            return new List<Review>()
            {
                new Review()
                {
                    Id=1,
                    ProductId=1,
                    CustomerId=Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"),
                    Rating=3,
                    ReviewText="Mnogo gotina masa"
                },
                new Review()
                {
                    Id=2,
                    ProductId=2,
                    CustomerId=Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"),
                    Rating=5,
                    ReviewText="Gotina plasma"
                }
            };
        }
    }
}
