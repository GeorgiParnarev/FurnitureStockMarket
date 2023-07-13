﻿namespace FurnitureStockMarket.Models.Product
{
    using System.ComponentModel.DataAnnotations;

    using static FurnitureStockMarket.Common.EntityValidationConstants.Product;

    public class AllProductsViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
