namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Database.Models.Account;
    using FurnitureStockMarket.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

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
            var model = new LoginViewModel();

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

            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (!(user is null))
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var transfer = new RegisterUserTransferModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password
            };

            StatusUserModel userStatus = await this.accountService.RegisterUserAsync(transfer);

            if (!userStatus.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", userStatus.Errors.Select(e => e.Description).ToList()));

                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.Username);

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            AddCustomerModel customerModel = new AddCustomerModel()
            {
                ApplicationUserId = user.Id,
                ShippingAddress = model.ShippingAddress,
                BillingAddress = model.BillingAddress
            };

            if (userStatus.Success)
            {
                await this.accountService.AddCustomerAsync(customerModel);

                this.TempData[SuccessMessage] = UserRegistrationSuccess;
            }
            else
            {
                this.TempData[ErrorMessage] = userStatus.Description;
                ModelState.AddModelError(string.Empty, userStatus.Description);
            }

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
