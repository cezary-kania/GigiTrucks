using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Products.Core.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddSubcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "products",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                schema: "products",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                schema: "products",
                table: "Categories",
                column: "CategoryId",
                principalSchema: "products",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                schema: "products",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryId",
                schema: "products",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "products",
                table: "Categories");
        }
    }
}
