namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Models.Admin;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

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

            if (categories.Count() == 0)
            {
                TempData[ErrorMessage] = "There are no existing categories!";

                RedirectToAction("AddCategory");
            }

            var model = new ChooseProductCategoryViewModel()
            {
                Categories = categories!
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseCategory(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data!");
                TempData[ErrorMessage] = "Invalid data!";

                var categories = await this.adminService.GetCategoriesAsync();

                var model = new ChooseProductCategoryViewModel()
                {
                    Categories = categories
                };

                return this.View(model);
            }

            try
            {
                return RedirectToAction("AddProduct", new { categoryId });
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

        [HttpGet]
        public async Task<IActionResult> AddProduct(int categoryId)
        {
            var subCategories = await this.adminService.GetSubCategoriesAsync(categoryId);

            if (subCategories.Count() == 0)
            {
                TempData[ErrorMessage] = "There are no existing sub-categories!";

                RedirectToAction("AddSubCategory");
            }

            var model = new AddProductViewModel()
            {
                SubCategories = subCategories!
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data!");

                var subCategories = await this.adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return this.View(model);
            }

            try
            {
                var transferModel = new AddProductsTransferModel()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    SubCategoryId = model.SubCategoryId,
                    Brand = model.Brand,
                    Quantity = model.Quantity,
                    ImageURL = model.ImageURL
                };

                await this.adminService.AddProductAsync(transferModel);

                TempData[SuccessMessage] = "Successfully added product";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid data!");

                var subCategories = await this.adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return this.View(model);
            }
        }
    }
}
