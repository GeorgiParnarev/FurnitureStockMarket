namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.MenuSearch;
    using Microsoft.AspNetCore.Mvc;

    public class MenuSearchController : Controller
    {
        private readonly IMenuSearchService menuSearchService;

        public MenuSearchController(IMenuSearchService menuSearchService)
        {
            this.menuSearchService = menuSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transferModel = await this.menuSearchService.GetAllCategoriesAsync();

            var model=new List<CategoriesViewModel>();

            foreach (var category in transferModel)
            {
                model.Add(new CategoriesViewModel()
                {
                    Category = category
                });
            }

            return this.View("Index", model);
        }
    }
}
