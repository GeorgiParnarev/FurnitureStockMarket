namespace FurnitureStockMarket.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class EditProductViewModel : AddProductViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
