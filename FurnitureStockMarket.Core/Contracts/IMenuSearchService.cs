namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Database.Models;

    public interface IMenuSearchService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(int categoryId);
    }
}
