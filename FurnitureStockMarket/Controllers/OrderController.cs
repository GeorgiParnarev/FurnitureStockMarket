namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Extensions;
    using FurnitureStockMarket.Models.Admin;
    using FurnitureStockMarket.Models.Order;
    using FurnitureStockMarket.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> MakeOrder(List<CartItemViewModel> cart)
        {
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
                orderSuccess = await this.orderService.CheckIfProductsStillExist(transferCart);
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
        public IActionResult AddOrder()
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
                Cart = cart
            };

            return this.View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddOrder(AddOrderViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData[ErrorMessage] = InvalidData;

        //        var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

        //        List<KeyValuePair<int, PaymentMethod>> paymentMethods = new List<KeyValuePair<int, PaymentMethod>>()
        //        {
        //            new KeyValuePair<int, PaymentMethod>(0, PaymentMethod.PayPal),
        //            new KeyValuePair<int, PaymentMethod>(1, PaymentMethod.CreditCard),
        //            new KeyValuePair<int, PaymentMethod>(2, PaymentMethod.DebitCard)
        //        };

        //        List<KeyValuePair<int, ShippingMethod>> shippingMethods = new List<KeyValuePair<int, ShippingMethod>>()
        //        {
        //            new KeyValuePair<int, ShippingMethod>(0, ShippingMethod.StandardShipping),
        //            new KeyValuePair<int, ShippingMethod>(1, ShippingMethod.ExpressShipping),
        //            new KeyValuePair<int, ShippingMethod>(2, ShippingMethod.StorePickup)
        //        };

        //        model.Cart = cart;
        //        model.PaymentMethods = paymentMethods;
        //        model.ShippingMethods = shippingMethods;

        //        return this.View(model);
        //    }

        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {
        //        TempData[ErrorMessage] = InvalidData;

        //        var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

        //        List<KeyValuePair<int, PaymentMethod>> paymentMethods = new List<KeyValuePair<int, PaymentMethod>>()
        //        {
        //            new KeyValuePair<int, PaymentMethod>(0, PaymentMethod.PayPal),
        //            new KeyValuePair<int, PaymentMethod>(1, PaymentMethod.CreditCard),
        //            new KeyValuePair<int, PaymentMethod>(2, PaymentMethod.DebitCard)
        //        };

        //        List<KeyValuePair<int, ShippingMethod>> shippingMethods = new List<KeyValuePair<int, ShippingMethod>>()
        //        {
        //            new KeyValuePair<int, ShippingMethod>(0, ShippingMethod.StandardShipping),
        //            new KeyValuePair<int, ShippingMethod>(1, ShippingMethod.ExpressShipping),
        //            new KeyValuePair<int, ShippingMethod>(2, ShippingMethod.StorePickup)
        //        };

        //        model.Cart = cart;
        //        model.PaymentMethods = paymentMethods;
        //        model.ShippingMethods = shippingMethods;

        //        return this.View(model);
        //    }
        //}
    }
}
