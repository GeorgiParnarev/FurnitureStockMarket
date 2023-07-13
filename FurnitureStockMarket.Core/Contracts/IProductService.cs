namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels;

    public interface IProductService
    {
        Task<IEnumerable<AllProductsTransferModel>> GetAllProductsAsync();
    }
}
