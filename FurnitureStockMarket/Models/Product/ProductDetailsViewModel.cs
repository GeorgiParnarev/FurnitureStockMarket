namespace FurnitureStockMarket.Models.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string Brand { get; set; } = null!;

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
