using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class edit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 8, 29, 53, 630, DateTimeKind.Utc).AddTicks(8852),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(4596));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 8, 29, 53, 631, DateTimeKind.Utc).AddTicks(394),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(7798));

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
                name: "FK_Order_Addresses_AddresseId",
                table: "Order",
                column: "AddresseId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Addresses_AddresseId",
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
                defaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(4596),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 8, 29, 53, 630, DateTimeKind.Utc).AddTicks(8852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(7798),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 8, 29, 53, 631, DateTimeKind.Utc).AddTicks(394));
        }
    }
}
