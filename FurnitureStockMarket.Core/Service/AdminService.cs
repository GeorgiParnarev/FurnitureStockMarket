namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Database;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {
        private readonly IRepository repo;

        public AdminService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddCategoryAsync(string name)
        {
            var newCategory = new Category()
            {
                Name = name
            };

            await this.repo.AddAsync(newCategory);
            await this.repo.SaveChangesAsync();
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

            await this.repo.AddAsync(newProduct);
            await this.repo.SaveChangesAsync();
        }

        public async Task AddSubCategoryAsync(AddSubCategoryTransferModel model)
        {
            var newSubCategory = new SubCategory()
            {
                CategoryId = model.CategoryId,
                Name = model.Name
            };

            await this.repo.AddAsync(newSubCategory);
            await this.repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync()
        {
            var Categories = await this.repo
                .AllReadonly<Category>()
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return Categories;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSubCategoriesAsync(int categoryId)
        {
            var subCategories = await this.repo
                .AllReadonly<SubCategory>()
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return subCategories;
        }
    }
}
