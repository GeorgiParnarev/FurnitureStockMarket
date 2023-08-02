namespace FurnitureStockMarket.Areas.Admin.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Category;

    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
