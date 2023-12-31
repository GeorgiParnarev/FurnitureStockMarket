﻿namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Review;
    using FurnitureStockMarket.Models.Review;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IOrderService orderService;

        public ReviewController(IReviewService reviewService,
            IOrderService orderService,
            IMenuSearchService menuSearchService) : base(menuSearchService)
        {
            this.reviewService = reviewService;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            var model = new AddProductReviewViewModel();

            model.ProductId = id;

            var customerId = await this.orderService.GetCustomerIdAsync(Guid.Parse(GetUserId()!));

            if (await this.reviewService.CheckIfCustomerAlreadyGaveAReviewAsync(customerId, id))
            {
                TempData[ErrorMessage] = AlreadyAddedReviewToProduct;

                return RedirectToAction("ProductDetails", "Home", new { id });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddProductReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = InvalidData;

                return this.View(model);
            }

            string reviewText = WebUtility.HtmlEncode(model.ReviewText);

            try
            {
                var customerId = await this.orderService.GetCustomerIdAsync(Guid.Parse(GetUserId()!));
                int id = model.ProductId;

                var transferModel = new AddProductReviewTransferModel()
                {
                    CustomerId = customerId,
                    ProductId = id,
                    Rating = model.Rating,
                    ReviewText = reviewText
                };

                await this.reviewService.AddProductReviewAsync(transferModel);

                TempData[SuccessMessage] = SuccessfullyAddedReview;

                return RedirectToAction("ProductDetails", "Home", new { id });
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = e.Message;

                return this.View(model);
            }
        }
    }
}
