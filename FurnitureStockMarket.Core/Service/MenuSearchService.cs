namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.MenuSearch;
    using FurnitureStockMarket.Core.Models.TransferModels.Product;
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

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = this.repo
                .AllReadonly<Category>()
                .Include(sb => sb.SubCategories);

            return categories;
        }

        public async Task<IEnumerable<AllProductMenuTransferModel>> GetAllProductsInCategory(int id)
        {
            var products = await this.repo
                .AllReadonly<Product>()
                .Where(p => p.Id == id)
                .Include(p=>p.SubCategory)
                .ThenInclude(p=>p.Category)
                .Select(p => new AllProductMenuTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL,
                    Category= p.SubCategory.Category                
                })
                .ToListAsync();

            return products;
        }
    }
}
