namespace FurnitureStockMarket.Core.Models.TransferModels.Order
{
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;

    public class MyOrdersTransferModel
    {
        public MyOrdersTransferModel()
        {
            this.ProductsOrders = new List<ProductsOrders>();
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public IEnumerable<ProductsOrders> ProductsOrders { get; set; }
    }
}
