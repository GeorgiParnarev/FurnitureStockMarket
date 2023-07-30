namespace FurnitureStockMarket.Core.Models.TransferModels.MenuSearch
{
    using FurnitureStockMarket.Database.Models;

    public class AllProductMenuTransferModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;

        public Category Category { get; set; } = null!;
    }
}
