namespace FurnitureStockMarket.Core.Service
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using FurnitureStockMarket.Database.Common;
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class AdminService : IAdminService
    {
        private readonly IRepository repo;

        public AdminService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddCategoryAsync(string name)
        {
            var newCategory = new Category()
            {
                Name = name
            };

            await this.repo.AddAsync(newCategory);
            await this.repo.SaveChangesAsync();
        }

        public async Task AddProductAsync(AddProductsTransferModel model)
        {
            var newProduct = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                SubCategoryId = model.SubCategoryId,
                Brand = model.Brand,
                Quantity = model.Quantity,
                ImageURL = model.ImageURL
            };

            await this.repo.AddAsync(newProduct);
            await this.repo.SaveChangesAsync();
        }

        public async Task AddSubCategoryAsync(AddSubCategoryTransferModel model)
        {
            var newSubCategory = new SubCategory()
            {
                CategoryId = model.CategoryId,
                Name = model.Name
            };

            await this.repo.AddAsync(newSubCategory);
            await this.repo.SaveChangesAsync();
        }

        public async Task EditProductAsync(EditProductTransferModel model)
        {
            var editProduct = await this.repo
                .All<Product>()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (editProduct is null)
            {
                throw new NullReferenceException(ProductNotExisting);
            }

            editProduct.Name = model.Name;
            editProduct.Description = model.Description;
            editProduct.Price = model.Price;
            editProduct.SubCategoryId = model.SubCategoryId;
            editProduct.Brand = model.Brand;
            editProduct.Quantity = model.Quantity;
            editProduct.ImageURL = model.ImageURL;

            await this.repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllOrdersTransferModel>> GetAllOrdersAsync()
        {
            var allOrders = await this.repo
                .AllReadonly<Order>()
                .Include(o => o.Customer)
                .ThenInclude(c => c.User)
                .Select(o => new AllOrdersTransferModel()
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    Customer = o.Customer,
                    TotalPrice = o.TotalPrice,
                    OrderStatus = o.OrderStatus,
                    PaymentMethod = o.PaymentMethod,
                    ShippingMethod = o.ShippingMethod
                })
                .ToListAsync();

            foreach (var order in allOrders)
            {
                order.ProductsOrders = await this.repo
                    .AllReadonly<ProductsOrders>()
                    .Where(po => po.OrderId == order.Id)
                    .Include(p => p.Product)
                    .Select(o => new ProductsOrders
                    {
                        Product = o.Product,
                        Quantity = o.Quantity
                    })
                    .ToListAsync();
            }

            return allOrders;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetCategoriesAsync()
        {
            var Categories = await this.repo
                .AllReadonly<Category>()
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return Categories;
        }

        public async Task<EditProductTransferModel> GetProductAsync(int id)
        {
            var newProduct = await this.repo
                .All<Product>()
                .Include(np => np.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (newProduct is null)
            {
                throw new Exception(ProductNotExisting);
            }

            var result = new EditProductTransferModel()
            {
                Id = id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                SubCategoryId = newProduct.SubCategoryId,
                CategoryId = newProduct.SubCategory.CategoryId,
                Brand = newProduct.Brand,
                Quantity = newProduct.Quantity,
                ImageURL = newProduct.ImageURL
            };

            return result;
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetSubCategoriesAsync(int categoryId)
        {
            var subCategories = await this.repo
                .AllReadonly<SubCategory>()
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name))
                .ToListAsync();

            return subCategories;
        }

        public async Task ShippingOrderAsync(Guid id)
        {
            var order = await this.repo
                .All<Order>()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                throw new NullReferenceException(OrderNotExisting);
            }

            if (order.OrderStatus == OrderStatus.Shipping)
            {
                throw new InvalidOperationException(OrderAlreadyShipping);
            }

            order.OrderStatus = OrderStatus.Shipping;

            await this.repo.SaveChangesAsync();
        }
    }
}
