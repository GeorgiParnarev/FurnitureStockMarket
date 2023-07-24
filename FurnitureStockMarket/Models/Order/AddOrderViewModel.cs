namespace FurnitureStockMarket.Models.Order
{
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Models.ShoppingCart;
    using System.ComponentModel.DataAnnotations;

    public class AddOrderViewModel
    {
        public AddOrderViewModel()
        {
            this.PaymentMethods = new List<KeyValuePair<int, PaymentMethod>>();
            this.ShippingMethods = new List<KeyValuePair<int, ShippingMethod>>();
            this.Cart = new List<CartItemViewModel>();
        }

        [Required]
        public Guid CustomerId { get; set; }

        public int PaymentId { get; set; }

        [Required]
        public IEnumerable<KeyValuePair<int, PaymentMethod>> PaymentMethods { get; set; }

        public int ShippingId { get; set; }

        [Required]
        public IEnumerable<KeyValuePair<int, ShippingMethod>> ShippingMethods { get; set; }

        public IEnumerable<CartItemViewModel> Cart { get; set; }
    }
}
