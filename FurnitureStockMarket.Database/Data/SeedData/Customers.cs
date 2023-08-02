namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Customers : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(CreateCustomers());
        }

        public IEnumerable<Customer> CreateCustomers()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id=Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"),
                    ApplicationUserId=Guid.Parse("BE4168B0-F7F1-4235-7063-08DB8C6611CB"),
                    ShippingAddress="U nas",
                    BillingAddress="U nas"
                },
                new Customer()
                {
                    Id=Guid.Parse("89E27DE8-58DC-41C2-4752-08DB8C8F85F5"),
                    ApplicationUserId=Guid.Parse("1A5BD1F2-EACD-4D95-C08C-08DB8C8F85D1"),
                    ShippingAddress="Ekonta na ugula",
                    BillingAddress="Ekonta na ugula"
                }
            };

        }
    }
}
