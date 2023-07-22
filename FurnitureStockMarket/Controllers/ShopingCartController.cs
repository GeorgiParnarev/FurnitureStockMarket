namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShopingCart;
    using FurnitureStockMarket.Extensions;
    using FurnitureStockMarket.Models.ShopingCart;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ShopingCartController : Controller
    {
        private readonly IShopingCartService shopingCartService;

        public ShopingCartController(IShopingCartService shopingCartService)
        {
            this.shopingCartService = shopingCartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            return View(cartItems);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            var product = new CartItemTransferModel();

            try
            {
                product = await this.shopingCartService.GetProductAsync(id);
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;
            }

            var transferModel = new CartItemTransferModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageURL = product.ImageURL
            };

            var transferCart = cart.Select(i => new CartItemTransferModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                ImageURL = i.ImageURL

            })
            .ToList();

            var updatedTransferCart = shopingCartService.AddToCart(transferCart, transferModel);

            cart = updatedTransferCart.Select(i => new CartItemViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                ImageURL = i.ImageURL
            })
            .ToList();

            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddMore(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart");

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
                var updatedTransferCart = await this.shopingCartService.AddOneMore(transferCart, id);

                cart = updatedTransferCart.Select(i => new CartItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageURL = i.ImageURL
                })
                .ToList();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;
            }

            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToAction("Index", "ShopingCart");
        }
    }
}
