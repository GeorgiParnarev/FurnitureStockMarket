namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Database;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly FurnitureStockMarketDbContext dbContext;

        public ProductService(FurnitureStockMarketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllProductsTransferModel>> GetAllProductsAsync()
        {
            var allProducts = await this.dbContext
                .Products
                .Select(p => new AllProductsTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL
                })
                .ToListAsync();

            return allProducts;
        }
    }
}
