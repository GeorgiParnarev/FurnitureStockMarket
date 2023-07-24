namespace FurnitureStockMarket.Database.Models
{
    using Models.Account;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Customer;

    public class Customer
    {
        public Customer()
        {
            this.Id = new Guid();
            this.Orders = new List<Order>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string ShippingAddress { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string BillingAddress { get; set; } = null!;

        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
