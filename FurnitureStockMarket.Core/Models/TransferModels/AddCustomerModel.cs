namespace FurnitureStockMarket.Core.Models.TransferModels
{
    public class AddCustomerModel
    {
        public string ShippingAddress { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        public Guid ApplicationUserId { get; set; }
    }
}
