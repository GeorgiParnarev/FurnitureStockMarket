namespace FurnitureStockMarket.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        //[Required]
        //public string Username { get; set; } = null!;

        [Required]
        public string EmailOrUsername { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
