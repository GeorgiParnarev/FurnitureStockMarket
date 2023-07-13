namespace FurnitureStockMarket.Common
{
    public static class EntityValidationConstants
    {
        public static class Customer
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 100;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;

            public const int EmailMinLength = 5;
            public const int EmailMaxLength = 255;

            public const int AddressMinLength = 3;
            public const int AddressMaxLength = 255;

            public const int UsernameMinLength = 5;
            public const int UsernameMaxLength = 30;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;

            public const int ShippingAddressMinLength = 5;
            public const int ShippingAddressMaxLength = 100;
        }

        public static class Product
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const string PriceMinValue = "0.01";
            public const string PriceMaxValue = "79228162514264337593543950335";

            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;

            public const string QuantityMinValue = "0";
            public const string QuantityMaxValue = "2147483647";

            public const int ImageURLMinLength = 6;
            public const int ImageURLMaxLength = 255;
        }

        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Review
        {
            public const int ReviewTextMinLength = 2;
            public const int ReviewTextMaxLength = 500;
        }
    }
}
