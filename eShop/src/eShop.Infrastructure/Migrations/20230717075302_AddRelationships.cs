using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products");

            migrationBuilder.AlterColumn<Guid>(
                name: "price_currency_id",
                table: "products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products",
                column: "price_currency_id",
                principalTable: "price_currency",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products");

            migrationBuilder.AlterColumn<Guid>(
                name: "price_currency_id",
                table: "products",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "fk_products_price_currency_price_currency_id",
                table: "products",
                column: "price_currency_id",
                principalTable: "price_currency",
                principalColumn: "id");
        }
    }
}
