namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<AddProductsTransferModel>> GetCategoriesAsync();
    }
}
