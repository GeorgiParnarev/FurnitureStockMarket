namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.Product;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tranferModel = await productService.GetAllProductsAsync();

            var model = tranferModel.Select(p => new AllProductsViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL
            });

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var transferModel = await productService.GetProductDetailsAsync(id);

            var model = new ProductDetailsViewModel()
            {
                Name = transferModel.Name,
                Description = transferModel.Description,
                Price = transferModel.Price,
                Brand = transferModel.Brand,
                Quantity = transferModel.Quantity,
                ImageURL = transferModel.ImageURL
            };

            return this.View(model);
        }
    }
}