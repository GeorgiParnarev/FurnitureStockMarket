namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Tests.UnitTests;
    using Microsoft.AspNetCore.Mvc.Diagnostics;
    using NUnit.Framework;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

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

        [Test]
        public async Task GetProductDetailsAsync_ShouldReturnProductDetails()
        {
            var expectedProduct = new Products().CreateProducts().First(p => p.Id == 1);

            var actualProduct = await this.homeService.GetProductDetailsAsync(1);

            Assert.That(actualProduct, Is.Not.Null);
            Assert.That(actualProduct.Name, Is.EqualTo(expectedProduct.Name));
            Assert.That(actualProduct.Description, Is.EqualTo(expectedProduct.Description));
            Assert.That(actualProduct.Price, Is.EqualTo(expectedProduct.Price));
            Assert.That(actualProduct.Brand, Is.EqualTo(expectedProduct.Brand));
            Assert.That(actualProduct.Quantity, Is.EqualTo(expectedProduct.Quantity));
            Assert.That(actualProduct.ImageURL, Is.EqualTo(expectedProduct.ImageURL));
        }

        [Test]
        public async Task GetProductDetailsAsync_ShouldThrownNullReferenceException()
        {
            var expectedMessage = ProductNotExisting;

            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await this.homeService.GetProductDetailsAsync(-1));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void GetProductReviewsAsync_ShouldReturnProductReviews()
        {
            var expectedReviews = new Reviews().CreateReviews().Where(p => p.ProductId == 1).ToList();

            var actualReviews = this.homeService.GetProductReviews(1).ToList();

            for (int i = 0; i < expectedReviews.Count(); i++)
            {
                Assert.That(actualReviews[i].Id, Is.EqualTo(expectedReviews[i].Id));
                Assert.That(actualReviews[i].Rating, Is.EqualTo(expectedReviews[i].Rating));
                Assert.That(actualReviews[i].ReviewText, Is.EqualTo(expectedReviews[i].ReviewText));
                Assert.That(actualReviews[i].ProductId, Is.EqualTo(expectedReviews[i].ProductId));
                Assert.That(actualReviews[i].CustomerId, Is.EqualTo(expectedReviews[i].CustomerId));
            }
        }

        [Test]
        public void GetProductReviewsAsync_ShouldReturnEmptyListIfThereAreNoReviews()
        {
            var expectedReviews = new Reviews().CreateReviews().Where(p => p.ProductId == -1).ToList();

            var actualReviews = this.homeService.GetProductReviews(-1).ToList();

            Assert.That(actualReviews.Count, Is.EqualTo(expectedReviews.Count));
        }
    }
}
