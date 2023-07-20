namespace FurnitureStockMarket.Core.Models.TransferModels.Account
{
    public class RegisterUserTransferModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
