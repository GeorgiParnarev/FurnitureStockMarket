namespace FurnitureStockMarket.Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Product;

    public class Product
    {
        public Product()
        {
            this.Reviews = new List<Review>();
            this.ProductsOrders = new List<ProductsOrders>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; } = null!;

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(ImageURLMaxLength)]
        public string ImageURL { get; set; } = null!;

        public IEnumerable<Review> Reviews { get; set; }

        public IEnumerable<ProductsOrders> ProductsOrders { get; set; }
    }
}
