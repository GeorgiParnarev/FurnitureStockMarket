﻿namespace FurnitureStockMarket.Controllers
{
    using FurnitureStockMarket.Controllers.BaseControllers;
    using FurnitureStockMarket.Core.Contracts;
    using FurnitureStockMarket.Core.Models.TransferModels.Review;
    using FurnitureStockMarket.Models.Review;
    using Microsoft.AspNetCore.Mvc;

    using static FurnitureStockMarket.Common.NotificationMessagesConstants;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IOrderService orderService;

        public ReviewController(IReviewService reviewService,
            IOrderService orderService)
        {
            this.reviewService = reviewService;
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult AddReview(int id)
        {
            var model = new AddProductReviewViewModel();

            model.ProductId = id;

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

            try
            {
                var transferModel = new AddProductReviewTransferModel()
                {
                    CustomerId = await this.orderService.GetCustomerIdAsync(Guid.Parse(GetUserId()!)),
                    ProductId = model.ProductId,
                    Rating = model.Rating,
                    ReviewText = model.ReviewText
                };

                await this.reviewService.AddProductReviewAsync(transferModel);

                TempData[SuccessMessage] = SuccessfullyAddedReview;

                int id = model.ProductId;

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