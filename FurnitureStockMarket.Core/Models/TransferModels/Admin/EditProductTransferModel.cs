namespace FurnitureStockMarket.Core.Models.TransferModels.Admin
{
    public class EditProductTransferModel : AddProductsTransferModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
    }
}
