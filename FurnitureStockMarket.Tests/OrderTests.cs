namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Tests.UnitTests;
    using Microsoft.EntityFrameworkCore;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class OrderTests : UnitTestsBase
    {
        private IOrderService orderService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.orderService = new OrderService(this.repo);
        }

        [Test]
        public async Task AddOrderAsync_SuccessfullyAddsOrder()
        {
            
            var expectedOrderCount = new Orders().CreateOrders().Count() + 1;

            string customerId = "89E27DE8-58DC-41C2-4752-08DB8C8F85F5";

            int productId = 1;
            string productName = "Big table";
            decimal productPrice = 25.00m;
            int productQuantity = 1;
            string productImageURL = "https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128";

            var AddedProduct = await this.repo.AllReadonly<Product>().FirstOrDefaultAsync(p => p.Id == productId);
            var expectedOrderedProductQuantity = AddedProduct!.Quantity-productQuantity;

            var model = new AddOrderTransferModel()
            {
                CustomerId = Guid.Parse(customerId),
                PaymentId = 1,
                ShippingId = 1,
                Cart = new List<CartItemTransferModel>()
                {
                    new CartItemTransferModel()
                    {
                        Id = productId,
                        Name = productName,
                        Price = productPrice,
                        Quantity = productQuantity,
                        ImageURL = productImageURL
                    }
                }
            };

            await this.orderService.AddOrderAsync(model);

            AddedProduct = await this.repo.AllReadonly<Product>().FirstOrDefaultAsync(p => p.Id == productId);
            var actualOrderedProductQuantity = AddedProduct!.Quantity;

            Assert.That(actualOrderedProductQuantity, Is.EqualTo(expectedOrderedProductQuantity));

            var actualOrderCount = this.repo
                .AllReadonly<Order>()
                .Count();

            Assert.That(actualOrderCount, Is.EqualTo(expectedOrderCount));

            var actualOrder = this.repo
                .AllReadonly<Order>()
                .Include(o => o.ProductsOrders)
                .First(p => p.CustomerId == Guid.Parse(customerId) && p.PaymentMethod == PaymentMethod.CreditCard && p.ShippingMethod == ShippingMethod.ExpressShipping);

            Assert.That(actualOrder, Is.Not.Null);

            Assert.That(actualOrder.ProductsOrders.First().ProductId == productId);
            Assert.That(actualOrder.ProductsOrders.First().Quantity == productQuantity);
        }

        [Test]
        public void CancelOrder_NullReferenceException_OrderNotExisting()
        {
            var expectedMessage = OrderNotExisting;

            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await this.orderService.CancelOrderAsync(Guid.Parse("e2307e2c-aad0-4437-add8-d9d0c96a7b7d")));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void CancelOrder_InvalidOperationException_CantCancelOrderAlreadyShipping()
        {
            var expectedMessage = CantCancelOrderAlreadyShipping;

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.orderService.CancelOrderAsync(Guid.Parse("c9bb3aa5-8f65-495d-8676-3d58f4f5f5ae")));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void CancelOrder_InvalidOperationException_FailedToCancelOrder()
        {
            var expectedMessage = FailedToCancelOrder;

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.orderService.CancelOrderAsync(Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f")));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task CancelOrder_SuccessfullyCancelsOrder()
        {
            var expectedOrderCount = new Orders().CreateOrders().Count() - 1;

            await this.orderService.CancelOrderAsync(Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f"));

            var actualOrderCount = this.repo
                .AllReadonly<Order>()
                .Count();

            Assert.That(actualOrderCount, Is.EqualTo(expectedOrderCount));
        }
    }
}