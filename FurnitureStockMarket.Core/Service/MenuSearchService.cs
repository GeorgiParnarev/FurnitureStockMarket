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

        public async Task<IEnumerable<AllProductsTransferModel>> GetAllProductsByTermAsync(string term)
        {
            string lowerCaseTerm=term.ToLower();

            var products = await this.repo
                .AllReadonly<Product>()
                .Where(p => p.Name.ToLower().Contains(lowerCaseTerm))
                .Select(p => new AllProductsTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<AllProductMenuCategoryTransferModel>> GetAllProductsInCategory(int id)
        {
            var products = await this.repo
                .AllReadonly<Product>()
                .Include(p => p.SubCategory)
                .ThenInclude(p => p.Category)
                .Where(p => p.SubCategory.CategoryId == id)
                .Select(p => new AllProductMenuCategoryTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL,
                    Category = p.SubCategory.Category
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<AllProductMenuSubCategoryTransferModel>> GetAllProductsInSubCategory(int id)
        {
            var products = await this.repo
                .AllReadonly<Product>()
                .Where(p => p.SubCategoryId == id)
                .Include(p => p.SubCategory)
                .Select(p => new AllProductMenuSubCategoryTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL,
                    SubCategory = p.SubCategory
                })
                .ToListAsync();

            return products;
        }
    }
}
