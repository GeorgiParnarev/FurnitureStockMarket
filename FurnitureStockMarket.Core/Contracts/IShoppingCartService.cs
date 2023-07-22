namespace FurnitureStockMarket.Core.Contracts
{
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;

    public interface IShoppingCartService
    {
        public Task<IEnumerable<CartItemTransferModel>> AddToCart(List<CartItemTransferModel> cart, CartItemTransferModel model);

        public Task<CartItemTransferModel> GetProductAsync(int id);

        public Task<IEnumerable<CartItemTransferModel>> AddOneMore(List<CartItemTransferModel> cart, int id);

        public IEnumerable<CartItemTransferModel> RemoveOneItem(List<CartItemTransferModel> cart, int id);
    }
}