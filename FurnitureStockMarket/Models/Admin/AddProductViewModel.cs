namespace FurnitureStockMarket.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Product;

    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            this.SubCategories = new List<KeyValuePair<int, string>>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public IEnumerable<KeyValuePair<int, string>> SubCategories { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(typeof(int), QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }

        [Required]
        [StringLength(ImageURLMaxLength, MinimumLength = ImageURLMinLength)]
        public string ImageURL { get; set; } = null!;
    }
}
