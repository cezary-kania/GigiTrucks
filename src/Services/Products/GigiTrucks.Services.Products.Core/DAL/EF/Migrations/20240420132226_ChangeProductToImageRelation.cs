using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Products.Core.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductToImageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Products_ProductId",
                schema: "products",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                schema: "products",
                table: "Price");

            migrationBuilder.RenameTable(
                name: "Price",
                schema: "products",
                newName: "Prices",
                newSchema: "products");

            migrationBuilder.RenameIndex(
                name: "IX_Price_ProductId",
                schema: "products",
                table: "Prices",
                newName: "IX_Prices_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "products",
                table: "Products",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Images",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "products",
                table: "Prices",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prices",
                schema: "products",
                table: "Prices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductId",
                schema: "products",
                table: "Prices",
                column: "ProductId",
                principalSchema: "products",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductId",
                schema: "products",
                table: "Prices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prices",
                schema: "products",
                table: "Prices");

            migrationBuilder.RenameTable(
                name: "Prices",
                schema: "products",
                newName: "Price",
                newSchema: "products");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_ProductId",
                schema: "products",
                table: "Price",
                newName: "IX_Price_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "products",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Images",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "products",
                table: "Price",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                schema: "products",
                table: "Price",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Products_ProductId",
                schema: "products",
                table: "Price",
                column: "ProductId",
                principalSchema: "products",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
