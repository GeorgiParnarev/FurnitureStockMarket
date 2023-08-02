namespace FurnitureStockMarket.Areas.Admin.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class EditProductViewModel : AddProductViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
