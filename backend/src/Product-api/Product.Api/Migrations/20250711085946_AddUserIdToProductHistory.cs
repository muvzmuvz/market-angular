using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToProductHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                table: "ProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProductHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductHistories");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductId",
                table: "ProductHistories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                table: "ProductHistories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
