using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigiTrucks.Services.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableColumnsToOrderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "orders",
                table: "OrderLine",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "orders",
                table: "OrderLine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                schema: "orders",
                table: "OrderLine",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "orders",
                table: "OrderLine",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "orders",
                table: "OrderLine");
        }
    }
}
