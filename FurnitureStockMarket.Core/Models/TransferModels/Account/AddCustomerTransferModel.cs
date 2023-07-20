namespace FurnitureStockMarket.Core.Models.TransferModels.Account
{
    public class AddCustomerTransferModel
    {
        public string ShippingAddress { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        public Guid ApplicationUserId { get; set; }
    }
}
