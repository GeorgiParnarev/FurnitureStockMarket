namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.ShopingCart;

    public interface IShopingCartService
    {
        public IEnumerable<CartItemTransferModel> AddToCart(List<CartItemTransferModel> cart, CartItemTransferModel model);

        public Task<CartItemTransferModel> GetProductAsync(int id);
    }
}
