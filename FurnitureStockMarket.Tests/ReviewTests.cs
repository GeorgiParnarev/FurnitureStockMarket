namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Review;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Tests.UnitTests;

    public class ReviewTests : UnitTestsBase
    {
        private IReviewService reviewService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.reviewService = new ReviewService(this.repo);
        }

        [Test]
        public async Task AddProductReviewAsync_SuccessfullyAddsReview()
        {
            var expectedReviewCount = new Reviews().CreateReviews().Count() + 1;

            var model = new AddProductReviewTransferModel()
            {
                CustomerId = Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"),
                ProductId = 1,
                Rating = 5,
                ReviewText = "Very cool table!"
            };

            await this.reviewService.AddProductReviewAsync(model);

            var actualReviewCount = this.repo
                .AllReadonly<Review>()
                .Count();

            Assert.That(actualReviewCount, Is.EqualTo(expectedReviewCount));
        }

        [Test]
        public async Task CheckIfCustomerAlreadyGaveAReview_ReturnsTrue()
        {
            bool expectedResult = true;

            bool actualResult = await this.reviewService.CheckIfCustomerAlreadyGaveAReviewAsync(Guid.Parse("756756FB-98B4-4E0A-9612-08DB8C661226"), 1);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task CheckIfCustomerAlreadyGaveAReview_ReturnsFalse()
        {
            bool expectedResult = false;

            bool actualResult = await this.reviewService.CheckIfCustomerAlreadyGaveAReviewAsync(Guid.Parse("89E27DE8-58DC-41C2-4752-08DB8C8F85F5"), 1);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}