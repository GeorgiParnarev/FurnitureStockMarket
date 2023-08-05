namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductsOrder : IEntityTypeConfiguration<ProductsOrders>
    {
        public void Configure(EntityTypeBuilder<ProductsOrders> builder)
        {
            builder.HasData(CreateProductsOrders());
        }

        public IEnumerable<ProductsOrders> CreateProductsOrders()
        {
            return new List<ProductsOrders>()
            {
                new ProductsOrders()
                {
                    OrderId = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"),
                    ProductId = 3,
                    Quantity = 5
                },
                new ProductsOrders()
                {
                    OrderId = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"),
                    ProductId = 2,
                    Quantity = 1
                },
                new ProductsOrders()
                {
                    OrderId = Guid.Parse("c9bb3aa5-8f65-495d-8676-3d58f4f5f5ae"),
                    ProductId = 1,
                    Quantity = 1
                }
            };
        }
    }
}
