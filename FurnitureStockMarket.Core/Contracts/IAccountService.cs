namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels;

    public interface IAccountService
    {
        public Task<StatusUserModel> RegisterUserAsync(RegisterUserTransferModel model);

        public Task AddCustomerAsync(AddCustomerModel customer);
    }
}
