namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Database;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {
        private readonly FurnitureStockMarketDbContext dbContext;

        public AdminService(FurnitureStockMarketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCategoryAsync(string name)
        {
            var newCategory = new Category()
            {
                Name = name
            };

            await dbContext.Categories.AddAsync(newCategory);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddProductAsync(AddProductsTransferModel model)
        {
            var newProduct = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                SubCategoryId = model.SubCategoryId,
                Brand = model.Brand,
                Quantity = model.Quantity,
                ImageURL = model.ImageURL
            };

            await dbContext.Products.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync()
        {
            var Categories = await dbContext
                .Categories
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return Categories;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSubCategoriesAsync(int categoryId)
        {
            var subCategories = await dbContext
                .SubCategories
                .Where(c => c.Id == categoryId)
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return subCategories;
        }
    }
}
