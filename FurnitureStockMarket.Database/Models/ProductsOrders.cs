namespace FurnitureStockMarket.Database.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductsOrders
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
