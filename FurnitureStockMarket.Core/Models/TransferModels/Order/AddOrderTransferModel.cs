namespace FurnitureStockMarket.Core.Models.TransferModels.Order
{
    using FurnitureStockMarket.Database.Enumerators;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using FurnitureStockMarket.Database.Models;

    public class AddOrderTransferModel
    {
        public AddOrderTransferModel()
        {
            this.PaymentMethods = new List<KeyValuePair<int, PaymentMethod>>();
            this.ShippingMethods = new List<KeyValuePair<int, ShippingMethod>>();
        }

        [Required]
        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        public decimal TotalPrice { get; set; }

        public int PaymentId { get; set; }

        [Required]
        public IEnumerable<KeyValuePair<int, PaymentMethod>> PaymentMethods { get; set; }

        public int ShippingId { get; set; }

        [Required]
        public IEnumerable<KeyValuePair<int, ShippingMethod>> ShippingMethods { get; set; }
    }
}
