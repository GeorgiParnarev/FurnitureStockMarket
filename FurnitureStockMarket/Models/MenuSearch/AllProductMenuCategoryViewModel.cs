namespace FurnitureStockMarket.Models.MenuSearch
{
    using FurnitureStockMarket.Database.Models;

    public class AllProductMenuCategoryViewModel
    {
        public AllProductMenuCategoryViewModel()
        {
            this.ProductReviews = new List<Review>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public IEnumerable<Review> ProductReviews { get; set; }

        public bool IsAdmin { get; set; }
    }
}
