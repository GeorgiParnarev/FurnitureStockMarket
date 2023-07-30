namespace FurnitureStockMarket.Common
{
    public static class NotificationMessagesConstants
    {
        public const string ErrorMessage = "ErrorMessage";
        public const string WarningMessage = "WarningMessage";
        public const string InformationMessage = "InformationMessage";
        public const string SuccessMessage = "SuccessMessage";

        public const string UserRegistrationSuccess = "Successfully registered user!";

        public const string UserRegistrationFail = "User registration fail!";
        public const string UsernameAlreadyExists = "Username already exists!";
        public const string EmailAlreadyExists = "Email already exists!";

        public const string InvalidData = "Invalid data!";

        public const string NoExistingCategory = "There are no existing categories!";
        public const string NoExistingSubCategory = "There are no existing sub-categories!";

        public const string SuccessfullyAddedProduct = "Successfully added product!";
        public const string SuccessfullyAddedCategory= "Successfully added category!";
        public const string SuccessfullyAddedSubCategory = "Successfully added sub-category!";
        public const string SuccessfullyAddedOrder = "Successfully added order!";

        public const string FailedToAddProduct = "Failed to add product!";
        public const string FailedToAddCategory = "Failed to add category!";
        public const string FailedToAddSubCategory = "Failed to add sub-category!";
        public const string FailedToAddOrder = "Failed to make order";

        public const string ProductNotExisting = "This product does not exist!";
        public const string SuccessfullyEditedProduct = "Successfully edited product!";
        public const string ProductStoredQuantityReached = "There are no units left of this product";
        public const string OneProductQuantityLeftInCart = "You can't lower the ammount of the product! You need to remove it from the cart!";

        public const string AlreadyOutOfStock = "{1} is out of stock";

        public const string CustomerNotExisting = "Customer does not exist";

        public const string OrderNotExisting = "Ordered is already canceled";
        public const string SuccessfullyShippingOrder = "Successfully shipping order!";
        public const string OrderAlreadyShipping = "Order is already shipping!";

        public const string CantCancelOrderAlreadyShipping = "You can't cancel the order because it's already shipping!";
        public const string SuccessfullyCanceledOrder = "Order canceled successfully!";
        public const string FailedToCancelOrder = "Failed to cancel the order!";

        public const string FailedToAddReview = "Failed to make a review!";
        public const string SuccessfullyAddedReview = "Successfully made review!";
        public const string AlreadyAddedReviewToProduct = "You already gave a review to this product!";

        public const string SuccessfullyAddedProductToCart = "Successfully added product to cart!";
    }
}
