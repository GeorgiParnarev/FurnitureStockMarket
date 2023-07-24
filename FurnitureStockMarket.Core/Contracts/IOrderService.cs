namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;

    public interface IOrderService
    {
        Task<bool> CheckIfProductsStillExist(List<CartItemTransferModel> model);

        //Task AddProductAsync(AddOrderTransferModel model);
    }
}
