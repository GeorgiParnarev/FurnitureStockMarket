namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Product;
    using FurnitureStockMarket.Database.Models;

    public interface IHomeService
    {
        Task<IEnumerable<AllProductsTransferModel>> GetAllProductsAsync();

        Task<ProductDetailsTransferModel> GetProductDetailsAsync(int id);

        IEnumerable<Review> GetProductReviews(int productId);
    }
}
