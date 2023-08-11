namespace FurnitureStockMarket.Areas.Admin.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;
    using static FurnitureStockMarket.Common.RoleConstants;
    using static FurnitureStockMarket.Common.GeneralApplicationConstants;
    using FurnitureStockMarket.Areas.Admin.Models.Admin;

    [Area(AdminAreaName)]
    [Authorize(Roles = Administrator)]
    [AutoValidateAntiforgeryToken]
    public class AdminController : BaseController
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> ChooseCategory(string data)
        {
            var categories = await adminService.GetCategoriesAsync();

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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseCategory(ChooseCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                var categories = await adminService.GetCategoriesAsync();

                model.Categories = categories;

                return View(model);
            }

            try
            {
                return RedirectToAction(model.Action, new { model.CategoryId });
            }
            catch (Exception)
            {
                var categories = await adminService.GetCategoriesAsync();

                model.Categories = categories;

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(int categoryId)
        {
            var subCategories = await adminService.GetSubCategoriesAsync(categoryId);

            if (subCategories.Count() == 0)
            {
                TempData[ErrorMessage] = NoExistingSubCategory;

                return RedirectToAction("AddSubCategory");
            }

            var model = new AddProductViewModel()
            {
                SubCategories = subCategories!
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return View(model);
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

                await adminService.AddProductAsync(transferModel);

                TempData[SuccessMessage] = SuccessfullyAddedProduct;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = FailedToAddProduct;

                var subCategories = await adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new AddCategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                return View(model);
            }

            try
            {
                await adminService.AddCategoryAsync(model.Name);

                TempData[SuccessMessage] = SuccessfullyAddedCategory;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = FailedToAddCategory;

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddSubCategory(int categoryId)
        {
            var model = new AddSubCategoryViewModel()
            {
                CategoryId = categoryId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(AddSubCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                return View(model);
            }

            try
            {
                var trasferModel = new AddSubCategoryTransferModel()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                };

                await adminService.AddSubCategoryAsync(trasferModel);

                TempData[SuccessMessage] = SuccessfullyAddedSubCategory;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = FailedToAddSubCategory;

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var editProduct = new EditProductViewModel();

            try
            {
                var transferEditProduct = await adminService.GetProductAsync(id);

                editProduct.Name = transferEditProduct.Name;
                editProduct.Description = transferEditProduct.Description;
                editProduct.Price = transferEditProduct.Price;
                editProduct.SubCategoryId = transferEditProduct.SubCategoryId;
                editProduct.Brand = transferEditProduct.Brand;
                editProduct.Quantity = transferEditProduct.Quantity;
                editProduct.ImageURL = transferEditProduct.ImageURL;
                editProduct.CategoryId = transferEditProduct.CategoryId;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var subCategories = await adminService.GetSubCategoriesAsync(editProduct.CategoryId);

            editProduct.SubCategories = subCategories;

            return View(editProduct);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return View(model);
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

                await adminService.EditProductAsync(transferModel);

                TempData[SuccessMessage] = SuccessfullyEditedProduct;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (NullReferenceException e)
            {
                TempData[ErrorMessage] = e.Message;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = InvalidData;

                var subCategories = await adminService.GetSubCategoriesAsync(model.CategoryId);

                model.SubCategories = subCategories;

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var transferModel = await adminService.GetAllOrdersAsync();

            var model = transferModel.Select(o => new AllOrdersViewModel()
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                Customer = o.Customer,
                TotalPrice = o.TotalPrice,
                OrderStatus = o.OrderStatus,
                PaymentMethod = o.PaymentMethod,
                ShippingMethod = o.ShippingMethod,
                ProductsOrders = o.ProductsOrders
            });

            return View(model);
        }

        public async Task<IActionResult> SendOrder(Guid id)
        {
            try
            {
                await adminService.ShippingOrderAsync(id);

                TempData[SuccessMessage] = SuccessfullyShippingOrder;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;
            }

            return RedirectToAction("AllOrders", "Admin");
        }
    }
}
