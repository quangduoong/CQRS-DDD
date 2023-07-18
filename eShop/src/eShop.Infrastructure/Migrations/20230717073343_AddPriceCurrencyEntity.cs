using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceCurrencyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_currency",
                table: "products");

            migrationBuilder.AddColumn<Guid>(
                name: "price_currency_id",
                table: "products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "price_currency",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price_currency", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "price_currency",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("1491857a-9602-44c4-bebb-80ef5e0ca81e"), "Vietnamese currency.", "VND" },
                    { new Guid("e347c43a-a547-42be-b134-5874454109a5"), "United State's currency.", "USD" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_price_currency_id",
                table: "products",
                column: "price_currency_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products",
                column: "price_currency_id",
                principalTable: "price_currency",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "price_currency");

            migrationBuilder.DropIndex(
                name: "ix_products_price_currency_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price_currency_id",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "price_currency",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
