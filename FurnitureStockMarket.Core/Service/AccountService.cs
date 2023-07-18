namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Database.Models.Account;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Database;
    using FurnitureStockMarket.Database.Common;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repo;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            IRepository repo)
        {
            this.userManager = userManager;
            this.repo = repo;
        }

        public async Task AddCustomerAsync(AddCustomerTransferModel customer)
        {
            var newCustomer = new Customer()
            {
                ApplicationUserId = customer.ApplicationUserId,
                ShippingAddress = customer.ShippingAddress,
                BillingAddress = customer.BillingAddress
            };

            await this.repo.AddAsync(newCustomer);
            await this.repo.SaveChangesAsync();
        }

        public async Task<StatusUserModel> RegisterUserAsync(RegisterUserTransferModel model)
        {
            StatusUserModel result = new StatusUserModel()
            {
                Success = false,
                Description = UserRegistrationFail
            };

            var user = await this.userManager.FindByNameAsync(model.Username);

            if (!(user is null))
            {
                result.Description = UsernameAlreadyExists;
                return result;
            }

            user = await this.userManager.FindByEmailAsync(model.Email);

            if (!(user is null))
            {
                result.Description = EmailAlreadyExists;
                return result;
            }

            var newUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var createStatus = await this.userManager.CreateAsync(newUser, model.Password);

            result.Success = createStatus.Succeeded;
            result.Errors = createStatus.Errors;

            return result;
        }
    }
}
