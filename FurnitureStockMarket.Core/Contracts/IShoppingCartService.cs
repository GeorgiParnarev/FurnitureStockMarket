namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;

    public interface IShoppingCartService
    {
        public Task<IEnumerable<CartItemTransferModel>> AddToCartAsync(List<CartItemTransferModel> cart, CartItemTransferModel model);

        public Task<CartItemTransferModel> GetProductAsync(int id);

        public Task<IEnumerable<CartItemTransferModel>> AddOneMoreAsync(List<CartItemTransferModel> cart, int id);

        public Task<IEnumerable<CartItemTransferModel>> RemoveOneItemAsync(List<CartItemTransferModel> cart, int id);

        public IEnumerable<CartItemTransferModel> RemoveProduct(List<CartItemTransferModel> cart, int id);
    }
}