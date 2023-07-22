namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Extensions;
    using FurnitureStockMarket.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
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
                product = await this.shoppingCartService.GetProductAsync(id);    
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

            try
            {
                var updatedTransferCart = await shoppingCartService.AddToCart(transferCart, transferModel);

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

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddMoreItem(int id)
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
                var updatedTransferCart = await this.shoppingCartService.AddOneMore(transferCart, id);

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

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
