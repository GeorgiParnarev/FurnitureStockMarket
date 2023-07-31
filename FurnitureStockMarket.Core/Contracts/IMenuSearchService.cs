namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.MenuSearch;
    using FurnitureStockMarket.Core.Models.TransferModels.Product;
    using FurnitureStockMarket.Database.Models;

    public interface IMenuSearchService
    {
        IEnumerable<Category> GetAllCategories();

        Task<IEnumerable<AllProductMenuCategoryTransferModel>> GetAllProductsInCategory(int id);

        Task<IEnumerable<AllProductMenuSubCategoryTransferModel>> GetAllProductsInSubCategory(int id);

        Task<IEnumerable<AllProductsTransferModel>> GetAllProductsByTermAsync(string term);
    }
}
