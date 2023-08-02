namespace FurnitureStockMarket.Database.Data.SeedData
{
    using FurnitureStockMarket.Database.Models.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUsers : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        public IEnumerable<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id=Guid.Parse("BE4168B0-F7F1-4235-7063-08DB8C6611CB"),
                    FirstName="Gogo",
                    LastName="Gogov",
                    UserName="GogoGogov20",
                    Email="GogoGogov@abv.bg",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber="0889256452",
                    NormalizedEmail="GogoGogov@abv.bg".ToUpper(),
                    NormalizedUserName="GogoGogov20".ToUpper()
                },
                new ApplicationUser()
                {
                    Id=Guid.Parse("1A5BD1F2-EACD-4D95-C08C-08DB8C8F85D1"),
                    FirstName="Misho",
                    LastName="Mishov",
                    UserName="MishoMishov",
                    Email="MishoMishov@abv.bg",
                    SecurityStamp= Guid.NewGuid().ToString(),
                    PhoneNumber="0889635423",
                    NormalizedEmail="MishoMishov@abv.bg".ToUpper(),
                    NormalizedUserName="MishoMishov".ToUpper()
                }
            };

            const string password = "123456";
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

            foreach (var user in users)
            {
                user.PasswordHash = hasher.HashPassword(user, password);
            }

            return users;
        }
    }
}
