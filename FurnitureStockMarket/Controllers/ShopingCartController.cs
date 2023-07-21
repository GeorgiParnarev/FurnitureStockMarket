namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShopingCart;
    using FurnitureStockMarket.Extensions;
    using FurnitureStockMarket.Models.ShopingCart;
    using Microsoft.AspNetCore.Mvc;

    public class ShopingCartController : Controller
    {
        private readonly IShopingCartService shopingCartService;

        public ShopingCartController(IShopingCartService shopingCartService)
        {
            this.shopingCartService = shopingCartService;
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            return View(cartItems);
        }

        public async Task<IActionResult> Add(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            var product = await this.shopingCartService.GetProductAsync(id);

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
    }
}
