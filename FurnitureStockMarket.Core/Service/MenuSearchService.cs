namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MenuSearchService : IMenuSearchService
    {
        private readonly IRepository repo;

        public MenuSearchService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = this.repo
                .AllReadonly<Category>();

            foreach (var category in categories)
            {
                category.SubCategories = await GetAllSubCategoriesAsync(category.Id);
            }                

            return categories;
        }

        public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(int categoryId)
        {
            var subCategories = await this.repo
                .AllReadonly<SubCategory>()
                .Where(sb => sb.CategoryId == categoryId)
                .ToListAsync();

            return subCategories;
        }
    }
}
