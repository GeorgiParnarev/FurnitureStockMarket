using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStockMarket.Database.Migrations
{
    public partial class initData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("efc44d01-81a8-4255-8499-5a86b16398c9"), "985c410e-0722-4129-9be4-35ba9d86534c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1a5bd1f2-eacd-4d95-c08c-08db8c8f85d1"), 0, "bf7be727-c4d8-49c2-ad30-6015d5ebff80", "MishoMishov@abv.bg", false, "Misho", "Mishov", false, null, "MISHOMISHOV@ABV.BG", "MISHOMISHOV", "AQAAAAEAACcQAAAAEGHhlqV6qhu8D+n2IiK7+ygg9Gp49qUYnDmZTEr1hi+CidEbntVeKKuuGCT9xH88GQ==", "0889635423", false, "562207e1-7690-4e10-8ef4-4f1b53c7a99b", false, "MishoMishov" },
                    { new Guid("be4168b0-f7f1-4235-7063-08db8c6611cb"), 0, "97e9fb51-c5e0-4004-ba12-d9a034dbb139", "GogoGogov@abv.bg", false, "Gogo", "Gogov", false, null, "GOGOGOGOV@ABV.BG", "GOGOGOGOV20", "AQAAAAEAACcQAAAAEKvb0vnI4d3f5afZYT9BRnpAyTonrRK6o/EuAz26iY0URCkZXT/+2TBWrN7aI9Kxgw==", "0889256452", false, "4b498cdc-7612-47aa-a70a-770b66029f5d", false, "GogoGogov20" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kitchen" },
                    { 2, "Livingroom" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("efc44d01-81a8-4255-8499-5a86b16398c9"), new Guid("be4168b0-f7f1-4235-7063-08db8c6611cb") });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ApplicationUserId", "BillingAddress", "ShippingAddress" },
                values: new object[,]
                {
                    { new Guid("756756fb-98b4-4e0a-9612-08db8c661226"), new Guid("be4168b0-f7f1-4235-7063-08db8c6611cb"), "U nas", "U nas" },
                    { new Guid("89e27de8-58dc-41c2-4752-08db8c8f85f5"), new Guid("1a5bd1f2-eacd-4d95-c08c-08db8c8f85d1"), "Ekonta na ugula", "Ekonta na ugula" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Tables" },
                    { 2, 2, "TV" },
                    { 3, 2, "Chair" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImageURL", "Name", "Price", "Quantity", "SubCategoryId" },
                values: new object[] { 1, "CoolTables", "Cool table!", "https://woodenwhaleworkshop.com/cdn/shop/products/image_492d397f-75a1-4c71-8302-406e5d2b847e_1170x.heic?v=1661569128", "Big table", 25.00m, 5, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImageURL", "Name", "Price", "Quantity", "SubCategoryId" },
                values: new object[] { 2, "Lenovo", "Mnogo qka plasma", "https://www.lg.com/ca_en/images/tvs/50pv400/gallery/medium08.jpg", "plasma", 1200.00m, 10, 2 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "ImageURL", "Name", "Price", "Quantity", "SubCategoryId" },
                values: new object[] { 3, "CoolChairs", "Cool chair!", "https://www.ikea.com/us/en/images/products/stefan-chair-brown-black__0727320_pe735593_s5.jpg?f=s", "Chair", 9.99m, 19, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("efc44d01-81a8-4255-8499-5a86b16398c9"), new Guid("be4168b0-f7f1-4235-7063-08db8c6611cb") });

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("756756fb-98b4-4e0a-9612-08db8c661226"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("89e27de8-58dc-41c2-4752-08db8c8f85f5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("efc44d01-81a8-4255-8499-5a86b16398c9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1a5bd1f2-eacd-4d95-c08c-08db8c8f85d1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("be4168b0-f7f1-4235-7063-08db8c6611cb"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
