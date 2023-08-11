namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Database.Models.Account;
    using FurnitureStockMarket.Models.MenuSearch;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Security.Claims;

    using static FurnitureStockMarket.Common.RoleConstants;

    [AutoValidateAntiforgeryToken]
    public class MenuSearchController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMenuSearchService menuSearchService;
        private readonly IHomeService productService;

        public MenuSearchController(UserManager<ApplicationUser> userManager, 
            IMenuSearchService menuSearchService,
            IHomeService productService)
        {
            this.userManager = userManager;
            this.menuSearchService = menuSearchService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProductsInCategory(int id)
        {
            var transferModel = await this.menuSearchService.GetAllProductsInCategory(id);

            bool isAdmin = false;

            var userId = GetUserId();

            if (userId is null)
            {
                isAdmin = false;
            }
            else
            {
                var user = this.userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));

                isAdmin = await this.userManager.IsInRoleAsync(user, Administrator);
            }

            var model = transferModel.Select(p => new AllProductMenuCategoryViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                Category = p.Category,
                ProductReviews = this.productService.GetProductReviews(p.Id),
                IsAdmin = isAdmin
            })
            .OrderByDescending(r => r.ProductReviews.Count().Equals(0) ? 0 : r.ProductReviews.Average(r => r.Rating));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllProductsInSubCategory(int id)
        {
            var transferModel = await this.menuSearchService.GetAllProductsInSubCategory(id);

            bool isAdmin = false;

            var userId = GetUserId();

            if (userId is null)
            {
                isAdmin = false;
            }
            else
            {
                var user = this.userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));

                isAdmin = await this.userManager.IsInRoleAsync(user, Administrator);
            }

            var model = transferModel.Select(p => new AllProductMenuSubCategoryViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                SubCategory = p.SubCategory,
                ProductReviews = this.productService.GetProductReviews(p.Id),
                IsAdmin = isAdmin
            })
            .OrderByDescending(r => r.ProductReviews.Count().Equals(0) ? 0 : r.ProductReviews.Average(r => r.Rating));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            string searchTermEncoded = WebUtility.HtmlEncode(searchTerm);

            var transferModel = await this.menuSearchService.GetAllProductsByTermAsync(searchTermEncoded);

            bool isAdmin = false;

            var userId = GetUserId();

            if (userId is null)
            {
                isAdmin = false;
            }
            else
            {
                var user = this.userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));

                isAdmin = await this.userManager.IsInRoleAsync(user, Administrator);
            }

            var model = transferModel.Select(p => new AllProductsByTermViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                SearchTerm = searchTermEncoded,
                ProductReviews = this.productService.GetProductReviews(p.Id),
                IsAdmin = isAdmin
            })
            .OrderByDescending(r => r.ProductReviews.Count().Equals(0) ? 0 : r.ProductReviews.Average(r => r.Rating));

            return this.View(model);
        }

        private string? GetUserId()
        {
            string? id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }
    }
}
