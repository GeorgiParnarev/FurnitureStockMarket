namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.Admin;
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> ChooseCategory()
        {
            var categories = await this.adminService.GetCategoriesAsync();

            var model = new ChooseProductCategoryViewModel()
            {
                Categories = categories
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseCategory(int CategoryId)
        {
            try
            {
                return RedirectToAction();
            }
            catch (Exception)
            {
                var categories = await this.adminService.GetCategoriesAsync();

                var model = new ChooseProductCategoryViewModel()
                {
                    Categories = categories
                };

                return this.View(model);
            }
        }
    }
}
