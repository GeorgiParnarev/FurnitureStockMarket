namespace FurnitureStockMarket.Areas.Admin.Models.Admin
{
    public class ChooseCategoryViewModel
    {
        public ChooseCategoryViewModel()
        {
            Categories = new List<KeyValuePair<int, string>>();
        }

        public int CategoryId { get; set; }

        public string Action { get; set; } = null!;

        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }
}
