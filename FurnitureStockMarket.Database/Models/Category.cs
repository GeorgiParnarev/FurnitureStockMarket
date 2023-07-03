namespace FurnitureStockMarker.Database.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            this.SubCategories = new List<SubCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
