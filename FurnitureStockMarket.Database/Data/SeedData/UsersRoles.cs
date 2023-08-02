namespace FurnitureStockMarket.Database.Data.SeedData
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UsersRoles : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(CreateUsersRoles());
        }

        public IdentityUserRole<Guid> CreateUsersRoles()
        {
            return new IdentityUserRole<Guid>()
            {
                RoleId = Guid.Parse("efc44d01-81a8-4255-8499-5a86b16398c9"),
                UserId = Guid.Parse("BE4168B0-F7F1-4235-7063-08DB8C6611CB")
            };
        }
    }
}
