namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Account;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Models.Account;
    using FurnitureStockMarket.Models.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAccountService accountService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService accountService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
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

            string emailOrUsername = WebUtility.HtmlEncode(model.EmailOrUsername);
            string password = WebUtility.HtmlEncode(model.Password);

            var user = await this.userManager.FindByEmailAsync(emailOrUsername);

            if (user is null)
            {
                user = await this.userManager.FindByNameAsync(emailOrUsername);
            }

            if (!(user is null))
            {
                var result = await this.signInManager.PasswordSignInAsync(user, password, false, false);

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = "/")
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string firstName=WebUtility.HtmlEncode(model.FirstName);
            string lastName=WebUtility.HtmlEncode(model.LastName);
            string username=WebUtility.HtmlEncode(model.Username);
            string email=WebUtility.HtmlEncode(model.Email);
            string phoneNumber=WebUtility.HtmlEncode(model.PhoneNumber);
            string password=WebUtility.HtmlEncode(model.Password);
            string shippingAddress=WebUtility.HtmlEncode(model.ShippingAddress);
            string billingAddress=WebUtility.HtmlEncode(model.BillingAddress);

            var transfer = new RegisterUserTransferModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = password
            };

            StatusUserModel userStatus = await this.accountService.RegisterUserAsync(transfer);

            if (!userStatus.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", userStatus.Errors.Select(e => e.Description).ToList()));

                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.Username);

            var result = await this.signInManager.PasswordSignInAsync(user, password, false, false);

            AddCustomerTransferModel customerModel = new AddCustomerTransferModel()
            {
                ApplicationUserId = user.Id,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress
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
            HttpContext.Session.Clear();

            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
