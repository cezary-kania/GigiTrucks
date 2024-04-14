using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "newsletter",
                table: "Subscribers");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "newsletter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriberId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalSchema: "newsletter",
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriberId",
                schema: "newsletter",
                table: "Subscriptions",
                column: "SubscriberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "newsletter");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "newsletter",
                table: "Subscribers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
