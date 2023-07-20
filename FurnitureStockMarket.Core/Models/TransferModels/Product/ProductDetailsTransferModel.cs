namespace FurnitureStockMarket.Core.Models.TransferModels.Product
{
    public class ProductDetailsTransferModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string Brand { get; set; } = null!;

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
