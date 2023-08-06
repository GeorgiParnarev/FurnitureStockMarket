namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
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
                var product = await this.repo
                    .All<Product>()
                    .FirstOrDefaultAsync(p => p.Id == item.Id);

                if (product is null)
                {
                    throw new NullReferenceException(ProductNotExisting);
                }

                product.Quantity -= item.Quantity;

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

        public async Task CancelOrderAsync(Guid id)
        {
            var order = await this.repo
                .All<Order>()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                throw new NullReferenceException(OrderNotExisting);
            }

            if (order.OrderStatus == OrderStatus.Shipping)
            {
                throw new InvalidOperationException(CantCancelOrderAlreadyShipping);
            }

            order.ProductsOrders = await this.repo
                .AllReadonly<ProductsOrders>()
                .Where(po => po.OrderId == order.Id)
                .Select(o => new ProductsOrders
                {
                    Product = o.Product,
                    ProductId = o.ProductId,
                    Order = o.Order,
                    OrderId = o.OrderId,
                    Quantity = o.Quantity
                })
                .ToListAsync();

            try
            {
                this.repo.Delete(order);
                this.repo.DeleteRange(order.ProductsOrders);

                foreach (var item in order.ProductsOrders)
                {
                    var product = await this.repo
                        .All<Product>()
                        .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                    if (product is null)
                    {
                        throw new NullReferenceException(ProductNotExisting);
                    }

                    product.Quantity += item.Quantity;
                }

                await this.repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InvalidOperationException(FailedToCancelOrder);
            }
        }

        public async Task<bool> CheckIfProductsStillExist(List<CartItemTransferModel> cart)
        {
            foreach (var item in cart)
            {
                var product = await this.repo
                    .All<Product>()
                    .FirstAsync(p => p.Id == item.Id);

                if (product.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException(string.Format(AlreadyOutOfStock, item.Name));
                }
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
                .Where(o => o.CustomerId == customerId)
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
