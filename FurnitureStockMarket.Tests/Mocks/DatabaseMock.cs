namespace FurnitureStockMarket.Tests.Mocks
{
    using FurnitureStockMarket.Database;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseMock
    {
        public static FurnitureStockMarketDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FurnitureStockMarketDbContext>()
                    .UseInMemoryDatabase($"FurnitureStockMarketDbContext {DateTime.Now.Ticks.ToString()}")
                    .Options;

                return new FurnitureStockMarketDbContext(dbContextOptions, false);
            }
        }
    }
}
