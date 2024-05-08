using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class addrese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 22, 0, 18, 762, DateTimeKind.Utc).AddTicks(65),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 21, 42, 31, 391, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 22, 0, 18, 762, DateTimeKind.Utc).AddTicks(1444),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 21, 42, 31, 391, DateTimeKind.Utc).AddTicks(5469));

            migrationBuilder.AddColumn<Guid>(
                name: "AddresseId",
                table: "Order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Order_AddresseId",
                table: "Order",
                column: "AddresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddresseId",
                table: "Order",
                column: "AddresseId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddresseId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AddresseId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddresseId",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 21, 42, 31, 391, DateTimeKind.Utc).AddTicks(3896),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 22, 0, 18, 762, DateTimeKind.Utc).AddTicks(65));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 21, 42, 31, 391, DateTimeKind.Utc).AddTicks(5469),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 22, 0, 18, 762, DateTimeKind.Utc).AddTicks(1444));
        }
    }
}
