using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Products.Core.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToSubCategoriesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                schema: "products",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "products",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId",
                schema: "products",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "products",
                table: "Categories",
                column: "ParentCategoryId",
                principalSchema: "products",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "products",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                schema: "products",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "products",
                table: "Categories",
                newName: "IX_Categories_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                schema: "products",
                table: "Categories",
                column: "CategoryId",
                principalSchema: "products",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
