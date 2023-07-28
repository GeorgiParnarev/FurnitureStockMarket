namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Review;

    public interface IReviewService
    {
        Task AddProductReviewAsync(AddProductReviewTransferModel model);

        Task<bool> CheckIfCustomerAlreadyGaveAReview(Guid customerId, int productId);
    }
}
