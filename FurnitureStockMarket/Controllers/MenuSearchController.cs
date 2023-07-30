namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.MenuSearch;
    using Microsoft.AspNetCore.Mvc;

    public class MenuSearchController : Controller
    {
        private readonly IMenuSearchService menuSearchService;
        private readonly IProductService productService;

        public MenuSearchController(IMenuSearchService menuSearchService,
            IProductService productService)
        {
            this.menuSearchService = menuSearchService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProductsInCategory(int id)
        {
            var transferModel = await this.menuSearchService.GetAllProductsInCategory(id);

            var model = transferModel.Select(p => new AllProductMenuViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                Category = p.Category,
                ProductReviews = this.productService.GetProductReviewsAsync(p.Id)
            });

            return this.View(model);
        }
    }
}
