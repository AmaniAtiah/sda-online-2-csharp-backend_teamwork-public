using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class editProductTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriesId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesId",
                table: "Products",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoriesId",
                table: "Products",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoriesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoriesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Products");

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
    }
}
