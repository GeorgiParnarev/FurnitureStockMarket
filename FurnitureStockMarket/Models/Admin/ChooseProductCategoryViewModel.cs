namespace FurnitureStockMarket.Models.Admin
{
    using FurnitureStockMarket.Database.Models;


    public class ChooseProductCategoryViewModel
    {
        public ChooseProductCategoryViewModel()
        {
            this.Categories = new List<KeyValuePair<int, string>>();
        }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }
}
