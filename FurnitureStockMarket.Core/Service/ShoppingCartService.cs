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

        public async Task<IEnumerable<CartItemTransferModel>> AddOneMoreAsync(List<CartItemTransferModel> cart, int id)
        {
            var checkIfAvaliable = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == id);

            var product = cart.FirstOrDefault(i => i.Id == id);

            if (checkIfAvaliable is null || product is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            if (checkIfAvaliable.Quantity == 0)
            {
                throw new InvalidOperationException(ProductStoredQuantityReached);
            }

            product.Quantity += AddDefaultProductAmmount;

            return cart;
        }

        public async Task<IEnumerable<CartItemTransferModel>> AddToCartAsync(List<CartItemTransferModel> cart, CartItemTransferModel model)
        {
            var product = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            var cartItem = cart.FirstOrDefault(i => i.Id == model.Id);

            if (product.Quantity == 0)
            {
                throw new InvalidOperationException(ProductStoredQuantityReached);
            }

            if (cartItem is null)
            {
                cart.Add(new CartItemTransferModel()

                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = AddDefaultProductAmmount,
                    ImageURL = model.ImageURL
                });
            }
            else
            {
                if (product.Quantity == 0)
                {
                    throw new InvalidOperationException(ProductStoredQuantityReached);
                }

                cartItem.Quantity += AddDefaultProductAmmount;
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

        public IEnumerable<CartItemTransferModel> RemoveOneItem(List<CartItemTransferModel> cart, int id)
        {
            var cartItem = cart.FirstOrDefault(i => i.Id == id);

            if (cartItem is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            if (cartItem.Quantity == 1)
            {
                throw new InvalidOperationException(OneProductQuantityLeftInCart);
            }

            cartItem.Quantity -= RemoveDefaultProductAmmount;

            return cart;
        }

        public IEnumerable<CartItemTransferModel> RemoveProduct(List<CartItemTransferModel> cart, int id)
        {
            var product = cart.FirstOrDefault(i => i.Id == id);

            if (product is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            cart.Remove(product);          

            return cart;
        }

    }
}
