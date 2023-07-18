namespace FurnitureStockMarket.Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Category;

    public class SubCategory
    {
        public SubCategory()
        {
            this.Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
