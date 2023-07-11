namespace FurnitureStockMarket.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        //[Required]
        //public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
