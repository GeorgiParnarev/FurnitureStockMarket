namespace FurnitureStockMarket.Controllers.BaseControllers
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Models.MenuSearch;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        protected readonly IMenuSearchService menuSearchService;

        public BaseController(IMenuSearchService menuSearchService)
        {
            this.menuSearchService = menuSearchService;
        }

        protected string? GetUserId()
        {
            string? id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var transferModel = this.menuSearchService.GetAllCategories();

            var model = new List<CategoriesViewModel>();

            foreach (var category in transferModel)
            {
                model.Add(new CategoriesViewModel()
                {
                    Category = category
                });
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedModel = JsonSerializer.Serialize(model, options);
            HttpContext.Session.SetString("Categories", serializedModel);

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
