namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Tests.UnitTests;
    using NUnit.Framework;

    [TestFixture]
    public class HomeTests : UnitTestsBase
    {
        private IHomeService homeService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.homeService = new HomeService(this.repo);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var expectedProducts = new Products().CreateProducts();

            var actualProducts = await this.homeService.GetAllProductsAsync();

            Assert.That(actualProducts.Count(), Is.EqualTo(expectedProducts.Count()));
        }
    }
}
