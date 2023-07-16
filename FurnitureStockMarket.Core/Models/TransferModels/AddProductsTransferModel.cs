namespace FurnitureStockMarket.Core.Models.TransferModels
{
    public class AddProductsTransferModel
    {
        public AddProductsTransferModel()
        {
            this.SubCategories = new List<KeyValuePair<int, string>>();
        }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<int, string>> SubCategories { get; set; }

        public string Brand { get; set; } = null!;

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
