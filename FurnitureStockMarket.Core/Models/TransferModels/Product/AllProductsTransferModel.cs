namespace FurnitureStockMarket.Core.Models.TransferModels.Product
{
    public class AllProductsTransferModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
