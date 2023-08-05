namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
        {
            this.homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tranferModel = await homeService.GetAllProductsAsync();

            var model = tranferModel.Select(p => new AllProductsViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                ProductReviews = this.homeService.GetProductReviews(p.Id)
            })
            .OrderByDescending(r => r.ProductReviews.Count().Equals(0) ? 0 : r.ProductReviews.Average(r => r.Rating));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var transferModel = await this.homeService.GetProductDetailsAsync(id);

            var productReviews = this.homeService.GetProductReviews(id);

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