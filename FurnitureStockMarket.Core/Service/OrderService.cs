namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Enumerators;
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

        public async Task AddOrderAsync(AddOrderTransferModel model)
        {
            var newOrder = new Order()
            {
                CustomerId = model.CustomerId,
                TotalPrice = model.Cart.Sum(item => item.Price * item.Quantity),
                OrderStatus = OrderStatus.Processing,
                PaymentMethod = (PaymentMethod)model.PaymentId,
                ShippingMethod = (ShippingMethod)model.ShippingId
            };

            var productOrders = new List<ProductsOrders>();

            foreach (var item in model.Cart)
            {
                productOrders.Add(new ProductsOrders()
                {
                    OrderId = newOrder.Id,
                    ProductId = item.Id,
                    Quantity = item.Quantity
                });
            }

            newOrder.ProductsOrders = productOrders;

            await this.repo.AddRangeAsync(productOrders);
            await this.repo.AddAsync(newOrder);
            await this.repo.SaveChangesAsync();
        }

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

        public async Task<Guid> GetCustomerIdAsync(Guid id)
        {
            var customer = await this.repo
                .AllReadonly<Customer>()
                .FirstOrDefaultAsync(c => c.ApplicationUserId == id);

            if (customer is null)
            {
                throw new NullReferenceException(CustomerNotExisting);
            }

            return customer.Id;
        }

        public async Task<IEnumerable<MyOrdersTransferModel>> GetMyOrdersAsync(Guid customerId)
        {
            var myOrders = await this.repo
                .AllReadonly<Order>()
                .Where(o=>o.CustomerId==customerId)
                .Include(o => o.Customer)
                .Select(o => new MyOrdersTransferModel()
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    Customer = o.Customer,
                    TotalPrice = o.TotalPrice,
                    OrderStatus = o.OrderStatus,
                    PaymentMethod = o.PaymentMethod,
                    ShippingMethod = o.ShippingMethod
                })
                .ToListAsync();

            foreach (var order in myOrders)
            {
                order.ProductsOrders = await this.repo
                    .AllReadonly<ProductsOrders>()
                    .Where(po => po.OrderId == order.Id)
                    .Include(p => p.Product)
                    .Select(o => new ProductsOrders
                    {
                        Product = o.Product,
                        Quantity = o.Quantity
                    })
                    .ToListAsync();
            }

            return myOrders;
        }
    }
}
