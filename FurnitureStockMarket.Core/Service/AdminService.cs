namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Database;
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

        public async Task<IEnumerable<KeyValuePair<int,string>>> GetCategoriesAsync()
        {
            var Categories = await dbContext
                .Categories
                .Select(c => new KeyValuePair<int, string>(c.Id,c.Name))
                .ToListAsync();

            return Categories;
        }
    }
}
