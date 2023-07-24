namespace FurnitureStockMarket.Database.Models
{
    using Enumerators;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public Order()
        {
            this.Id = new Guid();
            this.ProductsOrders = new List<ProductsOrders>();
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public ShippingMethod ShippingMethod { get; set; }

        public virtual IEnumerable<ProductsOrders> ProductsOrders { get; set; }
    }
}
