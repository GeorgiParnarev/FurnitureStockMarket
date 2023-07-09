namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarker.Database.Models.Account;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.StatusModels;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<StatusUserModel> RegisterUserAsync(RegisterUserTransferModel model)
        {
            StatusUserModel result = new StatusUserModel()
            {
                Success = false,
                Description = "User registration fail" //add constant
            };

            var user = await this.userManager.FindByNameAsync(model.Username);

            if (!(user is null))
            {
                result.Description = "Username already exists"; //add constant
                return result;
            }

            user = await this.userManager.FindByEmailAsync(model.Email);

            if (!(user is null))
            {
                result.Description = "Email already exists"; //add constant
                return result;
            }

            var newUser = new ApplicationUser()
            {
                FirstName = model.Username,
                LastName = model.Email,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var createStatus = await this.userManager.CreateAsync(newUser, model.Password);

            result.Success = true;

            return result;
        }
    }
}
