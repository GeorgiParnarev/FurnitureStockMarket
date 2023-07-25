namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using FurnitureStockMarket.Core.Models.TransferModels.Product;
    using FurnitureStockMarket.Database.Models;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync();

        Task<IEnumerable<KeyValuePair<int, string>>> GetSubCategoriesAsync(int categoryId);

        Task AddProductAsync(AddProductsTransferModel model);

        Task AddCategoryAsync(string name);

        Task AddSubCategoryAsync(AddSubCategoryTransferModel model);

        Task<EditProductTransferModel> GetProductAsync(int id);

        Task EditProductAsync(EditProductTransferModel model);

        Task<IEnumerable<AllOrdersTransferModel>> GetAllOrdersAsync();

        Task<Product> GetProductProductAsync(int id);

        Task ShippingOrder(Guid id);
    }
}
