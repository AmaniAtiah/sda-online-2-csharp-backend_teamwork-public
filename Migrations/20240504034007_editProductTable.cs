using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class editProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "categoriescategory_id",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoriescategory_id",
                table: "Products",
                column: "categoriescategory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoriescategory_id",
                table: "Products",
                column: "categoriescategory_id",
                principalTable: "Categories",
                principalColumn: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoriescategory_id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_categoriescategory_id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "categoriescategory_id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProductsId");
        }
    }
}
