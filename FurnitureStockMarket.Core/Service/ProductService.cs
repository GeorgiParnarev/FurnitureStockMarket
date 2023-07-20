﻿namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Product;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ProductService : IProductService
    {
        private readonly IRepository repo;

        public ProductService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<AllProductsTransferModel>> GetAllProductsAsync()
        {
            var allProducts = await this.repo
                .AllReadonly<Product>()
                .Select(p => new AllProductsTransferModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImageURL = p.ImageURL
                })
                .ToListAsync();

            return allProducts;
        }

        public async Task<ProductDetailsTransferModel> GetProductDetailsAsync(int id)
        {
            var product = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            var productDetails = new ProductDetailsTransferModel()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                Quantity = product.Quantity,
                ImageURL = product.ImageURL
            };

            var checkProductBeforeDisplaying = await this.repo
                .AllReadonly<Product>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (checkProductBeforeDisplaying is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            return productDetails;
        }
    }
}
