namespace FurnitureStockMarker.Database.Models.Account
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Customer;

    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
    }
}
