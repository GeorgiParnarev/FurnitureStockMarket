namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Order;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Extensions;
    using FurnitureStockMarket.Models.Order;
    using FurnitureStockMarket.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            bool orderSuccess = false;

            var transferCart = cart.Select(i => new CartItemTransferModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                ImageURL = i.ImageURL
            })
            .ToList();

            try
            {
                orderSuccess = await this.orderService.CheckIfProductsStillExistAsync(transferCart);
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;
            }

            if (orderSuccess)
            {
                return RedirectToAction("AddOrder", "Order");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            List<KeyValuePair<int, PaymentMethod>> paymentMethods = new List<KeyValuePair<int, PaymentMethod>>()
            {
                new KeyValuePair<int, PaymentMethod>(0, PaymentMethod.PayPal),
                new KeyValuePair<int, PaymentMethod>(1, PaymentMethod.CreditCard),
                new KeyValuePair<int, PaymentMethod>(2, PaymentMethod.DebitCard)
            };

            List<KeyValuePair<int, ShippingMethod>> shippingMethods = new List<KeyValuePair<int, ShippingMethod>>()
            {
                new KeyValuePair<int, ShippingMethod>(0, ShippingMethod.StandardShipping),
                new KeyValuePair<int, ShippingMethod>(1, ShippingMethod.ExpressShipping),
                new KeyValuePair<int, ShippingMethod>(2, ShippingMethod.StorePickup)
            };

            var model = new AddOrderViewModel()
            {
                PaymentMethods = paymentMethods,
                ShippingMethods = shippingMethods,
                CustomerId = await this.orderService.GetCustomerIdAsync(Guid.Parse(GetUserId()!)),
                Cart = cart
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderViewModel model)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                List<KeyValuePair<int, PaymentMethod>> paymentMethods = new List<KeyValuePair<int, PaymentMethod>>()
                {
                    new KeyValuePair<int, PaymentMethod>(0, PaymentMethod.PayPal),
                    new KeyValuePair<int, PaymentMethod>(1, PaymentMethod.CreditCard),
                    new KeyValuePair<int, PaymentMethod>(2, PaymentMethod.DebitCard)
                };

                List<KeyValuePair<int, ShippingMethod>> shippingMethods = new List<KeyValuePair<int, ShippingMethod>>()
                {
                    new KeyValuePair<int, ShippingMethod>(0, ShippingMethod.StandardShipping),
                    new KeyValuePair<int, ShippingMethod>(1, ShippingMethod.ExpressShipping),
                    new KeyValuePair<int, ShippingMethod>(2, ShippingMethod.StorePickup)
                };

                model.PaymentMethods = paymentMethods;
                model.ShippingMethods = shippingMethods;
                model.Cart = cart;

                return this.View(model);
            }

            try
            {
                var transferCart = cart.Select(i => new CartItemTransferModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageURL = i.ImageURL
                })
                .ToList();

                var transferModel = new AddOrderTransferModel()
                {
                    CustomerId = model.CustomerId,
                    PaymentId = model.PaymentId,
                    ShippingId = model.ShippingId,
                    Cart = transferCart
                };

                await this.orderService.AddOrderAsync(transferModel);

                HttpContext.Session.SetObject("Cart", (List<CartItemViewModel>)null!);

                TempData[SuccessMessage] = SuccessfullyAddedOrder;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = FailedToAddOrder;

                List<KeyValuePair<int, PaymentMethod>> paymentMethods = new List<KeyValuePair<int, PaymentMethod>>()
                {
                    new KeyValuePair<int, PaymentMethod>(0, PaymentMethod.PayPal),
                    new KeyValuePair<int, PaymentMethod>(1, PaymentMethod.CreditCard),
                    new KeyValuePair<int, PaymentMethod>(2, PaymentMethod.DebitCard)
                };

                List<KeyValuePair<int, ShippingMethod>> shippingMethods = new List<KeyValuePair<int, ShippingMethod>>()
                {
                    new KeyValuePair<int, ShippingMethod>(0, ShippingMethod.StandardShipping),
                    new KeyValuePair<int, ShippingMethod>(1, ShippingMethod.ExpressShipping),
                    new KeyValuePair<int, ShippingMethod>(2, ShippingMethod.StorePickup)
                };

                model.PaymentMethods = paymentMethods;
                model.ShippingMethods = shippingMethods;
                model.Cart = cart;

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            Guid customerId = await this.orderService.GetCustomerIdAsync(Guid.Parse(GetUserId()!));

            var transferModel = await this.orderService.GetMyOrdersAsync(customerId);

            var model = transferModel.Select(o => new MyOrdersViewModel()
            {
                Id = o.Id,
                Customer = o.Customer,
                TotalPrice = o.TotalPrice,
                OrderStatus = o.OrderStatus,
                PaymentMethod = o.PaymentMethod,
                ShippingMethod = o.ShippingMethod,
                ProductsOrders = o.ProductsOrders
            });

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                await this.orderService.CancelOrderAsync(id);

                TempData[SuccessMessage] = SuccessfullyCanceledOrder;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;
            }

            return RedirectToAction("MyOrders", "Order");
        }
    }
}
