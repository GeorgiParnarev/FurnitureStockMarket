namespace FurnitureStockMarker.Database.Models
{
    using FurnitureStockMarker.Database.Models.Account;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Customer;

    public class Customer
    {
        public Customer()
        {
            this.Id = new Guid();
            this.Orders=new List<Order>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid ApllicationUserId { get; set; }

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
