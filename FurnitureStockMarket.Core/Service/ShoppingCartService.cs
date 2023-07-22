namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;
    using static FurnitureStockMarket.Common.DefaultValuesConstants;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repo;

        public ShoppingCartService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<CartItemTransferModel>> AddOneMore(List<CartItemTransferModel> cart, int id)
        {
            var checkIfAvaliable = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == id);

            var product = cart.FirstOrDefault(i => i.Id == id);            

            if (product is null || checkIfAvaliable is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            if (checkIfAvaliable.Quantity == product.Quantity)
            {
                throw new Exception(ProductStoredQuantityReached);
            }

            product.Quantity += AddDefaultProductAmmount;

            return cart;
        }

        public IEnumerable<CartItemTransferModel> AddToCart(List<CartItemTransferModel> cart, CartItemTransferModel model)
        {
            var cartItem = cart.FirstOrDefault(i => i.Id == model.Id);

            if (cartItem is null)
            {
                cart.Add(new CartItemTransferModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ImageURL = model.ImageURL
                });
            }
            else
            {
                cartItem.Quantity += model.Quantity;
            }

            var updatedCart = cart.Select(i => new CartItemTransferModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                ImageURL = i.ImageURL
            });

            return updatedCart;
        }

        public async Task<CartItemTransferModel> GetProductAsync(int id)
        {
            var product = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            var result = new CartItemTransferModel()
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Quantity = AddDefaultProductAmmount,
                ImageURL = product.ImageURL
            };

            return result;
        }
    }
}
