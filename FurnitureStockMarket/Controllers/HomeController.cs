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
    }
}