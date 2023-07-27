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

        public async Task<Guid> GetCustomerIdAsync(Guid id)
        {
            var customer = await this.repo
                .AllReadonly<Customer>()
                .FirstOrDefaultAsync(c => c.ApplicationUserId == id);

            if (customer is null)
            {
                throw new NullReferenceException(CustomerNotExisting);
            }

            return customer.Id;
        }
    }
}
