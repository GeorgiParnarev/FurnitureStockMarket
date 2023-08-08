namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels;
    using FurnitureStockMarket.Core.Models.TransferModels.Admin;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Database.Enumerators;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Tests.UnitTests;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class AdminTests : UnitTestsBase
    {
        private IAdminService adminService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.adminService = new AdminService(this.repo);
        }

        [Test]
        public async Task AddCategoryAsync_SuccessfullyAddsCategory()
        {
            var expectedCount = new Categories().CreateCategories().Count() + 1;

            string categoryName = "Bathroom";

            await this.adminService.AddCategoryAsync(categoryName);

            var actualCount = this.repo
                .AllReadonly<Category>()
                .Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AddProductAsync_SuccessfullyAddsProduct()
        {
            var expectedCount = new Products().CreateProducts().Count() + 1;

            var model = new AddProductsTransferModel()
            {
                Name = "Desk",
                Description = "Cool desk!",
                Price = 12.99m,
                SubCategoryId = 1,
                Brand = "CoolDesks",
                Quantity = 10,
                ImageURL = "Image"
            };

            await this.adminService.AddProductAsync(model);

            var actualCount = this.repo
                .AllReadonly<Product>()
                .Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AddSubCategoryAsync_SuccessfullyAddsSubCategory()
        {
            var expectedCount = new SubCategories().CreateSubCategories().Count() + 1;

            var model = new AddSubCategoryTransferModel()
            {
                Name = "Desks",
                CategoryId = 2
            };

            await this.adminService.AddSubCategoryAsync(model);

            var actualCount = this.repo
                .AllReadonly<SubCategory>()
                .Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task EditProductAsync_SuccessfullyEditsProduct()
        {
            int id = 3;
            string name = "Not Chair";
            string description = "Not Cool chair!";
            decimal price = 39.99m;
            int subCategoryId = 3;
            string brand = "NotCoolChairs";
            int quantity = 10;
            string imageURL = "https://www.ikea.com/us/en/images/products/stefan-chair-brown-black__0727320_pe735593_s5.jpg?f=s";

            var model = new EditProductTransferModel()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                SubCategoryId = subCategoryId,
                Brand = brand,
                Quantity = quantity,
                ImageURL = imageURL
            };

            await this.adminService.EditProductAsync(model);

            var actualProduct = this.repo
                .AllReadonly<Product>()
                .First(p => p.Id == 3);

            Assert.That(actualProduct.Id, Is.EqualTo(id));
            Assert.That(actualProduct.Name, Is.EqualTo(name));
            Assert.That(actualProduct.Description, Is.EqualTo(description));
            Assert.That(actualProduct.Price, Is.EqualTo(price));
            Assert.That(actualProduct.SubCategoryId, Is.EqualTo(subCategoryId));
            Assert.That(actualProduct.Brand, Is.EqualTo(brand));
            Assert.That(actualProduct.Quantity, Is.EqualTo(quantity));
            Assert.That(actualProduct.ImageURL, Is.EqualTo(imageURL));
        }

        [Test]
        public void EditProductAsync_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var model = new EditProductTransferModel()
            {
                Id = -1,
                Name = "Not Chair",
                Description = "Not Cool chair!",
                Price = 39.99m,
                SubCategoryId = 3,
                Brand = "NotCoolChairs",
                Quantity = 10,
                ImageURL = "https://www.ikea.com/us/en/images/products/stefan-chair-brown-black__0727320_pe735593_s5.jpg?f=s"
            };

            var exceptionCart = Assert.ThrowsAsync<NullReferenceException>(async () => await this.adminService.EditProductAsync(model));

            Assert.That(exceptionCart.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task GetAllOrdersAsync_SuccessfullyReturnsAllOrders()
        {
            var expectedCount = new Orders().CreateOrders().Count();

            var allOrders = await this.adminService.GetAllOrdersAsync();

            var actualCount = allOrders.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetCategoriesAsync_SuccessfullyReturnsAllCategories()
        {
            var expectedCount = new Categories().CreateCategories().Count() + 1;

            var allCategories = await this.adminService.GetCategoriesAsync();

            var actualCount = allCategories.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetProductAsync_SuccessfullyReturnsProduct()
        {
            var id = 2;
            var name = "plasma";
            var description = "Mnogo qka plasma";
            var price = 1200.00m;
            var subCategoryId = 2;
            var brand = "Lenovo";
            var quantity = 10;
            var imageURL = "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg";

            var actualProduct = await this.adminService.GetProductAsync(id);

            Assert.That(actualProduct.Id, Is.EqualTo(id));
            Assert.That(actualProduct.Name, Is.EqualTo(name));
            Assert.That(actualProduct.Description, Is.EqualTo(description));
            Assert.That(actualProduct.Price, Is.EqualTo(price));
            Assert.That(actualProduct.SubCategoryId, Is.EqualTo(subCategoryId));
            Assert.That(actualProduct.Brand, Is.EqualTo(brand));
            Assert.That(actualProduct.Quantity, Is.EqualTo(quantity));
            Assert.That(actualProduct.ImageURL, Is.EqualTo(imageURL));
        }

        [Test]
        public void GetProductAsync_Exception_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var exceptionCart = Assert.ThrowsAsync<Exception>(async () => await this.adminService.GetProductAsync(-1));

            Assert.That(exceptionCart.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task GetSubCategoriesAsync_ReturnsAllSubCategoriesInCategory()
        {
            int categoryId = 1;

            var expectedCount = new SubCategories().CreateSubCategories().Where(sb => sb.CategoryId == categoryId).Count();

            var allSubCategoriesInCategory = await this.adminService.GetSubCategoriesAsync(categoryId);

            var actualCount = allSubCategoriesInCategory.Count();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task ShippingOrderAsync_SuccessfullyShipsOrder()
        {
            var expectedResult = OrderStatus.Shipping;

            var orderId = Guid.Parse("eb88cbc5-1cd2-4840-a209-32560b18271f");

            await this.adminService.ShippingOrderAsync(orderId);

            var order = this.repo
                .AllReadonly<Order>()
                .First(o => o.Id == orderId);

            Assert.That(order.OrderStatus, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShippingOrderAsync_NullReferenceException_OrderNotExisting()
        {
            var expectedMessage = OrderNotExisting;

            var exceptionCart = Assert.ThrowsAsync<NullReferenceException>(async () => await this.adminService.ShippingOrderAsync(Guid.Parse("d9026909-d85d-4ba1-8279-43e34b9210e1")));

            Assert.That(exceptionCart.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void ShippingOrderAsync_InvalidOperationException_OrderAlreadyShipping()
        {
            var expectedMessage = OrderAlreadyShipping;

            var exceptionCart = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.adminService.ShippingOrderAsync(Guid.Parse("c9bb3aa5-8f65-495d-8676-3d58f4f5f5ae")));

            Assert.That(exceptionCart.Message, Is.EqualTo(expectedMessage));
        }
    }
}
