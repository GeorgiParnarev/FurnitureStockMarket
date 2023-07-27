namespace FurnitureStockMarket.Models.Review
{
    using FurnitureStockMarket.Database.Models;
    using System.ComponentModel.DataAnnotations;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Review;

    public class AddProductReviewViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; set; }

        [Required]
        [StringLength(ReviewTextMaxLength, MinimumLength = ReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;
    }
}
