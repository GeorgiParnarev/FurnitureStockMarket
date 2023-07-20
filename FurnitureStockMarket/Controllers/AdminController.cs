namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
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
        public async Task<IActionResult> ChooseCategory(string data)
        {
            var categories = await this.adminService.GetCategoriesAsync();

            if (categories.Count() == 0)
            {
                TempData[ErrorMessage] = NoExistingCategory;

                return RedirectToAction("AddCategory");
            }

            var model = new ChooseCategoryViewModel()
            {
                Action = data,
                Categories = categories!
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseCategory(ChooseCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                var categories = await this.adminService.GetCategoriesAsync();

                model.Categories = categories;

                return this.View(model);
            }

            try
            {
                return RedirectToAction(model.Action, new { model.CategoryId });
            }
            catch (Exception)
            {
                var categories = await this.adminService.GetCategoriesAsync();

                model.Categories = categories;

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int categoryId)
        {
            var subCategories = await this.adminService.GetSubCategoriesAsync(categoryId);

            if (subCategories.Count() == 0)
            {
                TempData[ErrorMessage] = NoExistingSubCategory;

                return RedirectToAction("AddSubCategory");
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
                TempData[ErrorMessage] = InvalidData;

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

                TempData[SuccessMessage] = SuccessfullyAddedProduct;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await this.adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new AddCategoryViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                return this.View(model);
            }

            try
            {
                await this.adminService.AddCategoryAsync(model.Name);

                TempData[SuccessMessage] = SuccessfullyAddedCategory;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = InvalidData;

                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult AddSubCategory(int categoryId)
        {
            var model = new AddSubCategoryViewModel()
            {
                CategoryId = categoryId
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(AddSubCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                return this.View(model);
            }

            try
            {
                var trasferModel = new AddSubCategoryTransferModel()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                };

                await this.adminService.AddSubCategoryAsync(trasferModel);

                TempData[SuccessMessage] = SuccessfullyAddedSubCategory;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = InvalidData;

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var editProduct=new EditProductViewModel();

            try
            {
                var transferEditProduct = await this.adminService.GetProductAsync(id);

                editProduct.Name = transferEditProduct.Name;
                editProduct.Description = transferEditProduct.Description;
                editProduct.Price = transferEditProduct.Price;
                editProduct.SubCategoryId = transferEditProduct.SubCategoryId;
                editProduct.Brand=transferEditProduct.Brand;
                editProduct.Quantity = transferEditProduct.Quantity;
                editProduct.ImageURL= transferEditProduct.ImageURL;
                editProduct.CategoryId= transferEditProduct.CategoryId;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;

                return RedirectToAction("Index", "Home");
            }

            var subCategories = await this.adminService.GetSubCategoriesAsync(editProduct.CategoryId);

            editProduct.SubCategories = subCategories;

            return this.View(editProduct);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await this.adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return this.View(model);
            }

            try
            {
                var transferModel = new EditProductTransferModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    SubCategoryId = model.SubCategoryId,
                    Brand = model.Brand,
                    Quantity = model.Quantity,
                    ImageURL = model.ImageURL
                };

                await this.adminService.EditProductAsync(transferModel);

                TempData[SuccessMessage] = SuccessfullyEditedProduct;

                return RedirectToAction("Index", "Home");
            }
            catch (NullReferenceException e)
            {
                TempData[ErrorMessage] = e.Message;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await this.adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return this.View(model);
            }
        }
    }
}
