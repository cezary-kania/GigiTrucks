using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Products.Core.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContentTypeFromProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "products",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "products",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
