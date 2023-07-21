namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShopingCart;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ShopingCartService : IShopingCartService
    {
        private readonly IRepository repo;

        public ShopingCartService(IRepository repo)
        {
            this.repo = repo;
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
                Quantity = 1, //should constant be added
                ImageURL = product.ImageURL
            };

            return result;
        }
    }
}
