namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Database.Models.Account;
    using FurnitureStockMarket.Models;
    using FurnitureStockMarket.Models.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    using static FurnitureStockMarket.Common.RoleConstants;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
            IHomeService homeService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
        {
            this.userManager = userManager;
            this.homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tranferModel = await homeService.GetAllProductsAsync();

            bool isAdmin = false;

            var id = GetUserId();

            if (id is null)
            {
                isAdmin = false;
            }
            else
            {
                var user = this.userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(id));

                isAdmin = await this.userManager.IsInRoleAsync(user, Administrator);
            }

            var model = tranferModel.Select(p => new AllProductsViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                ProductReviews = this.homeService.GetProductReviews(p.Id),
                IsAdmin = isAdmin
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

        [Route("/Home/Error")]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode == 401)
            {
                return RedirectToAction("Error401", "Home");
            }
            else if (statusCode == 404)
            {
                return RedirectToAction("Error404", "Home");
            }
            else if (statusCode == 500)
            {
                return RedirectToAction("Error500", "Home");
            }
            else
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        public IActionResult Error401()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error500()
        {
            return View();
        }
    }
}