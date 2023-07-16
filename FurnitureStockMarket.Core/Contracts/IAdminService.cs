namespace FurnitureStockMarket.Core.Contracts
{
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync();
    }
}
