﻿namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;

    public interface IOrderService
    {
        Task<bool> CheckIfProductsStillExistAsync(List<CartItemTransferModel> model);

        Task AddOrderAsync(AddOrderTransferModel model);

        Task<Guid> GetCustomerIdAsync(Guid id);

        Task<IEnumerable<MyOrdersTransferModel>> GetMyOrdersAsync(Guid customerId);

        Task CancelOrderAsync(Guid id);
    }
}
