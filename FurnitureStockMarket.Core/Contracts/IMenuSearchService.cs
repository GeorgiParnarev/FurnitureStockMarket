namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.MenuSearch;
    using FurnitureStockMarket.Database.Models;

    public interface IMenuSearchService
    {
        IEnumerable<Category> GetAllCategories();

        Task<IEnumerable<AllProductMenuTransferModel>> GetAllProductsInCategory(int id);
    }
}
