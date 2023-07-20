namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Product;

    public interface IProductService
    {
        Task<IEnumerable<AllProductsTransferModel>> GetAllProductsAsync();

        Task<ProductDetailsTransferModel> GetProductDetailsAsync(int id);
    }
}
