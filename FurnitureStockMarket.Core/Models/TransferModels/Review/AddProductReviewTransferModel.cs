namespace FurnitureStockMarket.Core.Models.TransferModels.Review
{
    public class AddProductReviewTransferModel
    {
        public Guid CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; } = null!;
    }
}
