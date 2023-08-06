namespace FurnitureStockMarket.Tests
{
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.ShoppingCart;
    using FurnitureStockMarket.Core.Service;
    using FurnitureStockMarket.Tests.UnitTests;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ShoppingCartTests : UnitTestsBase
    {
        private IShoppingCartService shoppingCartService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.shoppingCartService = new ShoppingCartService(this.repo);
        }

        [Test]
        public async Task AddOneMoreAsync_SuccessfullyAddsOneMoreToTheQuantity()
        {
            var expectedQuantity = 2;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 1,
                },
                new CartItemTransferModel
                {
                    Id = 2,
                    Quantity = 3
                }
            };

            int id = 1;

            var actualCart = await this.shoppingCartService.AddOneMoreAsync(cart, id);

            var actualQuantity = actualCart.ToList().First(p => p.Id == id).Quantity;

            Assert.That(actualQuantity, Is.EqualTo(expectedQuantity));
        }


        [Test]
        public void AddOneMoreAsync_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 1,
                },
                new CartItemTransferModel
                {
                    Id = 2,
                    Quantity = 3
                }
            };

            int id = -1;

            var exceptionDatabase = Assert.ThrowsAsync<NullReferenceException>(async () => await this.shoppingCartService.AddOneMoreAsync(cart, id));

            Assert.That(exceptionDatabase.Message, Is.EqualTo(expectedMessage));

            id = 3;

            var exceptionCart = Assert.ThrowsAsync<NullReferenceException>(async () => await this.shoppingCartService.AddOneMoreAsync(cart, id));

            Assert.That(exceptionCart.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void AddOneMoreAsync_InvalidOperationException_ProductStoredQuantityReached()
        {
            var expectedMessage = ProductStoredQuantityReached;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 2,
                    Quantity = 10,
                }
            };

            int id = 2;

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.shoppingCartService.AddOneMoreAsync(cart, id));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task AddToCartAsync_SuccessfullyAddsProductToCart()
        {
            var expectedCount = 2;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 2,
                }
            };

            var model = new CartItemTransferModel()
            {
                Id = 2,
                Quantity = 2,
                Name = "plasma",
                Price = 1200.00m,
                ImageURL = "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg"
            };

            var updatedCart = await this.shoppingCartService.AddToCartAsync(cart, model);

            Assert.That(updatedCart.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AddToCartAsync_SuccessfullyAddsProductQuantityToCart()
        {
            var expectedCount = 3;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 2,
                }
            };

            var model = new CartItemTransferModel()
            {
                Id = 1,
                Quantity = 1,
                Name = "plasma",
                Price = 1200.00m,
                ImageURL = "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg"
            };

            var updatedCart = await this.shoppingCartService.AddToCartAsync(cart, model);

            Assert.That(updatedCart.First(p=>p.Id==model.Id).Quantity, Is.EqualTo(expectedCount));
        }

        [Test]
        public void AddToCartAsync_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 2,
                }
            };

            var model = new CartItemTransferModel()
            {
                Id = -1,
                Quantity = 2,
                Name = "plasma",
                Price = 1200.00m,
                ImageURL = "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg"
            };

            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await this.shoppingCartService.AddToCartAsync(cart, model));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void AddToCartAsync_InvalidOperationException_ProductStoredQuantityReached_ProductNotInCart()
        {
            var expectedMessage = ProductStoredQuantityReached;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 1,
                    Quantity = 2,
                }
            };

            var model = new CartItemTransferModel()
            {
                Id = 4,
                Quantity = 2,
                Name = "plasma",
                Price = 1200.00m,
                ImageURL = "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg"
            };

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.shoppingCartService.AddToCartAsync(cart, model));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void AddToCartAsync_InvalidOperationException_ProductStoredQuantityReached_ProductInCart()
        {
            var expectedMessage = ProductStoredQuantityReached;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 3,
                    Quantity = 19,
                }
            };

            var model = new CartItemTransferModel()
            {
                Id = 3,
                Quantity = 1,
                Name = "Chair",
                Price = 9.99m,
                ImageURL = "https://www.ikea.com/us/en/images/products/stefan-chair-brown-black__0727320_pe735593_s5.jpg?f=s"
            };

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.shoppingCartService.AddToCartAsync(cart, model));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task GetProductAsync_ReturnsTheProduct()
        {
            var actualProduct = await this.shoppingCartService.GetProductAsync(1);

            Assert.That(actualProduct.Id, Is.EqualTo(1));
            Assert.That(actualProduct.Name, Is.EqualTo("Big table"));
            Assert.That(actualProduct.Price, Is.EqualTo(25.00m));
            Assert.That(actualProduct.Quantity, Is.EqualTo(1));
            Assert.That(actualProduct.ImageURL, Is.EqualTo("https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128"));
        }

        [Test]
        public void GetProductAsync_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var exception = Assert.ThrowsAsync<NullReferenceException>(async () => await this.shoppingCartService.GetProductAsync(-1));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void RemoveOneItem_SuccessfullyRemovesOneProduct()
        {
            var expectedCount = 2;

            int productId = 3;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = productId,
                    Quantity = 3,
                }
            };

            var updatedCart = this.shoppingCartService.RemoveOneItem(cart, productId);

            Assert.That(updatedCart.First(p => p.Id == productId).Quantity, Is.EqualTo(expectedCount));
        }

        [Test]
        public void RemoveOneItem_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 3,
                    Quantity = 3,
                }
            };

            var exception = Assert.Throws<NullReferenceException>(() => this.shoppingCartService.RemoveOneItem(cart, -1));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void RemoveOneItem_InvalidOperationException_OneProductQuantityLeftInCart()
        {
            var expectedMessage = OneProductQuantityLeftInCart;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 5,
                    Quantity = 1,
                }
            };

            var exception = Assert.Throws<InvalidOperationException>(() => this.shoppingCartService.RemoveOneItem(cart, 5));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void RemoveProduct_SuccessfullyRemovesProductFromCart()
        {
            var expectedCount = 0;

            int productId = 3;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = productId,
                    Quantity = 3,
                }
            };

            var updatedCart = this.shoppingCartService.RemoveProduct(cart, productId);

            Assert.That(updatedCart.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public void RemoveProduct_NullReferenceException_ProductNotExisting()
        {
            var expectedMessage = ProductNotExisting;

            var cart = new List<CartItemTransferModel>()
            {
                new CartItemTransferModel
                {
                    Id = 3,
                    Quantity = 3,
                }
            };

            var exception = Assert.Throws<NullReferenceException>(() => this.shoppingCartService.RemoveProduct(cart, 5));

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
    }
}