namespace FurnitureStockMarket.Models.Order
{
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Models.ShoppingCart;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AddOrderViewModel
    {
        public AddOrderViewModel()
        {
            this.PaymentMethods = new List<KeyValuePair<int, PaymentMethod>>();
            this.ShippingMethods = new List<KeyValuePair<int, ShippingMethod>>();
            this.Cart = new List<CartItemViewModel>();
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

        public IEnumerable<CartItemViewModel> Cart { get; set; }
    }
}
