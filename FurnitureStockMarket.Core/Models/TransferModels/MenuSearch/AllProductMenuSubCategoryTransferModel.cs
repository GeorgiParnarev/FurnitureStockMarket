using FurnitureStockMarket.Database.Models;

namespace FurnitureStockMarket.Core.Models.TransferModels.MenuSearch
{
    public class AllProductMenuSubCategoryTransferModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;

        public SubCategory SubCategory { get; set; } = null!;
    }
}
