namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync();

        Task<IEnumerable<KeyValuePair<int, string>>> GetSubCategoriesAsync(int categoryId);

        Task AddProductAsync(AddProductsTransferModel model);
    }
}
