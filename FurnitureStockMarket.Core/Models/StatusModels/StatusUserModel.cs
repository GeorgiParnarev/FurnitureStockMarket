using Microsoft.AspNetCore.Identity;

namespace FurnitureStockMarket.Core.Models.StatusModels
{
    public class StatusUserModel
    {
        public StatusUserModel()
        {
            this.Errors = new List<IdentityError>();
        }

        public bool Success { get; set; }

        public string Description { get; set; } = null!;

        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
