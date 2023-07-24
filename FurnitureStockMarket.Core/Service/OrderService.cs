namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

        public OrderService(IRepository repo)
        {
            this.repo = repo;
        }

        //public Task AddProductAsync(AddOrderTransferModel model)
        //{
        //    var newOrder = new Order()
        //    {
        //        CustomerId = model.CustomerId,
        //        TotalPrice = model.TotalPrice
        //    };
        //}

        public async Task<bool> CheckIfProductsStillExist(List<CartItemTransferModel> cart)
        {
            foreach (var item in cart)
            {
                var product = await this.repo
                    .All<Product>()
                    .FirstOrDefaultAsync(p => p.Id == item.Id);

                if (product is null)
                {
                    throw new InvalidOperationException(string.Format(AlreadyOutOfStock, item.Name));
                }

                product.Quantity -= item.Quantity;
            }

            return true;
        }
    }
}
