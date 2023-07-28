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
                ImageURL = p.ImageURL,
                ProductReviews = this.productService.GetProductReviewsAsync(p.Id)
            })
            .OrderByDescending(r => r.ProductReviews.Average(r => r.Rating));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var transferModel = await this.productService.GetProductDetailsAsync(id);

            var productReviews = this.productService.GetProductReviewsAsync(id);

            var model = new ProductDetailsViewModel()
            {
                Id = id,
                Name = transferModel.Name,
                Description = transferModel.Description,
                Price = transferModel.Price,
                Brand = transferModel.Brand,
                Quantity = transferModel.Quantity,
                ImageURL = transferModel.ImageURL,
                ProductReviews = productReviews
            };

            return this.View(model);
        }
    }
}