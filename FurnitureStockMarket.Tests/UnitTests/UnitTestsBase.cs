namespace FurnitureStockMarket.Tests.UnitTests
{
    using FurnitureStockMarket.Database;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Tests.Mocks;

    public class UnitTestsBase
    {
        protected FurnitureStockMarketDbContext dbContext;
        protected IRepository repo;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.dbContext = DatabaseMock.Instance;
            this.repo = new Repository(this.dbContext);
            this.SeedDataBase();
        }

        private void SeedDataBase()
        {
            this.dbContext.Roles.Add(new ApplicationUserRoles().CreateRole());
            this.dbContext.Users.AddRange(new ApplicationUsers().CreateUsers());
            this.dbContext.Customers.AddRange(new Customers().CreateCustomers());
            this.dbContext.UserRoles.AddRange(new UsersRoles().CreateUsersRoles());
            this.dbContext.Categories.AddRange(new Categories().CreateCategories());
            this.dbContext.SubCategories.AddRange(new SubCategories().CreateSubCategories());
            this.dbContext.Products.AddRange(new Products().CreateProducts());
            this.dbContext.Reviews.AddRange(new Reviews().CreateReviews());
            this.dbContext.Orders.AddRange(new Orders().CreateOrders());
            this.dbContext.ProductsOrders.AddRange(new ProductsOrder().CreateProductsOrders());

            this.dbContext.SaveChanges();
        }
    }
}
