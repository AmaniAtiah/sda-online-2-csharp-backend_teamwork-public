using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(4596),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 7, 0, 833, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(7798),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 7, 0, 833, DateTimeKind.Utc).AddTicks(9773));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 7, 7, 0, 833, DateTimeKind.Utc).AddTicks(8835),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(4596));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 8, 7, 7, 0, 833, DateTimeKind.Utc).AddTicks(9773),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 5, 8, 7, 28, 42, 753, DateTimeKind.Utc).AddTicks(7798));
        }
    }
}
