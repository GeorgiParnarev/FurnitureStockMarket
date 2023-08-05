namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Database.Data.SeedData;
    using FurnitureStockMarket.Database.Models;
    using FurnitureStockMarket.Tests.UnitTests;

    [TestFixture]
    public class MenuSearchTests : UnitTestsBase
    {
        private IMenuSearchService menuSearchService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.menuSearchService = new MenuSearchService(this.repo);
        }

        [Test]
        public void GetAllCategories_ShouldReturnAllCategoriesWithSubcategories()
        {
            var expectedCategories = new Categories().CreateCategories().ToList();
            var expectedSubCategories = new SubCategories().CreateSubCategories().ToList();

            var actualCategories = this.menuSearchService.GetAllCategories().ToList();

            Assert.That(actualCategories.Count(), Is.EqualTo(expectedCategories.Count()));

            int subCategoriesCount = 0;

            for (int i = 0; i < actualCategories.Count(); i++)
            {
                subCategoriesCount += actualCategories[i].SubCategories.Count();
            }

            Assert.That(expectedSubCategories.Count, Is.EqualTo(subCategoriesCount));
        }

        [Test]
        public async Task GetAllProductsByTermAsync_ShouldReturnProductsMatchingTerm()
        {
            string searchTerm = "i";

            var expectedProducts = new Products().CreateProducts().Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            var actualProducts = await this.menuSearchService.GetAllProductsByTermAsync(searchTerm);

            Assert.That(actualProducts.Count(), Is.EqualTo(expectedProducts.Count));

            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.That(actualProducts.ToList()[i].Id, Is.EqualTo(expectedProducts[i].Id));
                Assert.That(actualProducts.ToList()[i].Quantity, Is.EqualTo(expectedProducts[i].Quantity));
                Assert.That(actualProducts.ToList()[i].Price, Is.EqualTo(expectedProducts[i].Price));
                Assert.That(actualProducts.ToList()[i].ImageURL, Is.EqualTo(expectedProducts[i].ImageURL));
                Assert.That(actualProducts.ToList()[i].Name, Is.EqualTo(expectedProducts[i].Name));
            }
        }

        [Test]
        public async Task GetAllProductsByTermAsync_ShouldReturnNoProductsIfNoMatchingTerm()
        {
            string searchTerm = "123";

            var expectedProducts = new Products().CreateProducts().Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            var actualProducts = await this.menuSearchService.GetAllProductsByTermAsync(searchTerm);

            Assert.That(actualProducts.Count(), Is.EqualTo(expectedProducts.Count));
        }

        [Test]
        public async Task GetAllProductsInCategory_ShouldReturnProductsInCategory()
        {
            var expectedProducts = new List<Product>()
            {
                new Product()
                {
                    Id=1,
                    Name="Big table",
                    Description="Cool table!",
                    Price=25.00m,
                    SubCategoryId=1,
                    Brand="CoolTables",
                    Quantity=5,
                    ImageURL="https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128"
                }
            };

            int categoryId = 1;

            var actualProducts = await this.menuSearchService.GetAllProductsInCategory(categoryId);

            Assert.That(actualProducts.Count(), Is.EqualTo(expectedProducts.Count));

            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.That(actualProducts.ToList()[i].Id, Is.EqualTo(expectedProducts[i].Id));
                Assert.That(actualProducts.ToList()[i].Quantity, Is.EqualTo(expectedProducts[i].Quantity));
                Assert.That(actualProducts.ToList()[i].Price, Is.EqualTo(expectedProducts[i].Price));
                Assert.That(actualProducts.ToList()[i].ImageURL, Is.EqualTo(expectedProducts[i].ImageURL));
                Assert.That(actualProducts.ToList()[i].Name, Is.EqualTo(expectedProducts[i].Name));
            }
        }

        [Test]
        public async Task GetAllProductsInSubCategory_ShouldReturnProductsInSubCategory()
        {
            var expectedProducts = new List<Product>()
            {
                new Product()
                {
                    Id=1,
                    Name="Big table",
                    Description="Cool table!",
                    Price=25.00m,
                    SubCategoryId=1,
                    Brand="CoolTables",
                    Quantity=5,
                    ImageURL="https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128"
                }
            };

            int subCategoryId = 1;

            var actualProducts = await this.menuSearchService.GetAllProductsInSubCategory(subCategoryId);

            Assert.That(actualProducts.Count(), Is.EqualTo(expectedProducts.Count));

            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.That(actualProducts.ToList()[i].Id, Is.EqualTo(expectedProducts[i].Id));
                Assert.That(actualProducts.ToList()[i].Quantity, Is.EqualTo(expectedProducts[i].Quantity));
                Assert.That(actualProducts.ToList()[i].Price, Is.EqualTo(expectedProducts[i].Price));
                Assert.That(actualProducts.ToList()[i].ImageURL, Is.EqualTo(expectedProducts[i].ImageURL));
                Assert.That(actualProducts.ToList()[i].Name, Is.EqualTo(expectedProducts[i].Name));
            }
        }
    }
}
