namespace FurnitureStockMarket.Core.Models.TransferModels
{
    public class RegisterUserTransferModel
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
