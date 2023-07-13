namespace FurnitureStockMarket.Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidationConstants.Product;

    public class Product
    {
        public Product()
        {
            this.Reviews = new List<Review>();
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
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }

        public virtual Order? Order { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(ImageURLMaxLength)]
        public string ImageURL { get; set; } = null!;

        public IEnumerable<Review> Reviews { get; set; }
    }
}
