namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Products : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateProducts());
        }

        public IEnumerable<Product> CreateProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name="Big table",
                    Description="Cool table!",
                    Price=25.00m,
                    SubCategoryId=1,
                    Brand="CoolTables",
                    Quantity=5,
                    ImageURL="https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128"
                },
                new Product
                {
                    Id = 2,
                    Name="plasma",
                    Description="Mnogo qka plasma",
                    Price=1200.00m,
                    SubCategoryId=2,
                    Brand="Lenovo",
                    Quantity=10,
                    ImageURL="https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name="Chair",
                    Description="Cool chair!",
                    Price=9.99m,
                    SubCategoryId=3,
                    Brand="CoolChairs",
                    Quantity=19,
                    ImageURL="https://www.ikea.com/us/en/images/products/stefan-chair-brown-black__0727320_pe735593_s5.jpg?f=s"
                },
                new Product
                {
                    Id = 4,
                    Name="Kitchen Chair",
                    Description="Cool kitchen chair!",
                    Price=15.00m,
                    SubCategoryId=3,
                    Brand="CoolChairs",
                    Quantity=0,
                    ImageURL="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.thebrick.com%2Fproducts%2Ftalia-dining-chair&psig=AOvVaw1xRpIrpwl-F0B4IUn3ezxt&ust=1691371222915000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCPiLiPruxoADFQAAAAAdAAAAABAE"
                }
            };
        }
    }
}
