namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Orders : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(CreateOrders());
        }

        public IEnumerable<Order> CreateOrders()
        {
            return new List<Order>()
            {
                new Order
                {
                    Id = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"),
                    CustomerId = Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"),
                    //ProductsOrders = new List<ProductsOrders>()
                    //{
                    //    new ProductsOrders()
                    //    {
                    //        OrderId = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"),
                    //        ProductId = 3,
                    //        Quantity = 5
                    //    },
                    //    new ProductsOrders()
                    //    {
                    //        OrderId = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"),
                    //        ProductId = 2,
                    //        Quantity = 1
                    //    }
                    //},
                    TotalPrice = (1200 + 9.99m * 5),
                    OrderStatus = OrderStatus.Processing,
                    PaymentMethod = PaymentMethod.PayPal,
                    ShippingMethod = ShippingMethod.StandardShipping
                },
                new Order
                {
                    Id = Guid.Parse("c9bb3aa5-8f65-495d-8676-3d58f4f5f5ae"),
                    CustomerId = Guid.Parse("89E27DE8-58DC-41C2-4752-08DB8C8F85F5"),
                    //ProductsOrders = new List<ProductsOrders>()
                    //{
                    //    new ProductsOrders()
                    //    {
                    //        OrderId = Guid.Parse("c9bb3aa5-8f65-495d-8676-3d58f4f5f5ae"),
                    //        ProductId = 1,
                    //        Quantity = 1
                    //    }
                    //},
                    TotalPrice = 25.00m,
                    OrderStatus = OrderStatus.Shipping,
                    PaymentMethod = PaymentMethod.CreditCard,
                    ShippingMethod = ShippingMethod.ExpressShipping
                }
            };
        }
    }
}
