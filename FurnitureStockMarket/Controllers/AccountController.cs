namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarker.Database.Models.Account;
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAccountService accountService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService accountService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.Username);

            if (!(user is null))
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (!(model.ReturnUrl is null))
                    {
                        return this.Redirect(model.ReturnUrl);
                    }
                }

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string? returnUrl = "/")
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    var transfer = new RegisterUserTransferModel()
        //    {
        //        Username = model.Username,
        //        Email = model.Email,
        //        Password = model.Password
        //    };

        //    StatusUserModel userStatus= await this.accountService
        //}
    }
}
