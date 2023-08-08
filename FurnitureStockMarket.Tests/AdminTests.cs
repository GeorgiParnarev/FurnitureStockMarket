namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Tests.UnitTests;

    public class AdminTests : UnitTestsBase
    {
        private IAdminService adminService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.adminService = new AdminService(this.repo);
        }
    }
}
