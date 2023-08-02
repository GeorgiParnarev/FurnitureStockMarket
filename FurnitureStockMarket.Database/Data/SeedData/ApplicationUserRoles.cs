namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models.Account;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static FurnitureStockMarket.Common.RoleConstants;

    public class ApplicationUserRoles : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(CreateRole());
        }

        public ApplicationRole CreateRole()
        {
            return new ApplicationRole()
            {
                Id = Guid.Parse("efc44d01-81a8-4255-8499-5a86b16398c9"),
                Name = Administrator,
                NormalizedName = Administrator.ToUpper(),
            };
        }
    }
}
