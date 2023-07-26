namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;

    public interface IOrderService
    {
        Task<bool> CheckIfProductsStillExist(List<CartItemTransferModel> model);

        Task AddOrderAsync(AddOrderTransferModel model);

        Task<Guid> GetCustomerIdAsync(Guid id);

        Task<IEnumerable<MyOrdersTransferModel>> GetMyOrdersAsync(Guid customerId);
    }
}
