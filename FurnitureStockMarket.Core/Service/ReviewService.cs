namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Review;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ReviewService : IReviewService
    {
        private readonly IRepository repo;

        public ReviewService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddProductReviewAsync(AddProductReviewTransferModel model)
        {
            var newReview = new Review()
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            };

            try
            {
                await this.repo.AddAsync(newReview);
                await this.repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InvalidOperationException(FailedToAddReview);
            }
        }

        public async Task<bool> CheckIfCustomerAlreadyGaveAReviewAsync(Guid customerId, int productId)
        {
            var review = await this.repo
                .AllReadonly<Review>()
                .Where(r => r.CustomerId == customerId)
                .FirstOrDefaultAsync(r => r.ProductId == productId);

            if (review is null)
            {
                return false;
            }
            
            return true;
        }
    }
}
