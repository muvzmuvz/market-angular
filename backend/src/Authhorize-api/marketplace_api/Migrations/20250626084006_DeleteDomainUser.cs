using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace_api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDomainUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_DomainUser_OwnerId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSellers_DomainUser_SellerId",
                table: "ShopSellers");

            migrationBuilder.DropTable(
                name: "DomainUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shops");

            migrationBuilder.AddColumn<decimal>(
                name: "ExpenseSummary",
                table: "AspNetUsers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_AspNetUsers_OwnerId",
                table: "Shops",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSellers_AspNetUsers_SellerId",
                table: "ShopSellers",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_AspNetUsers_OwnerId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSellers_AspNetUsers_SellerId",
                table: "ShopSellers");

            migrationBuilder.DropColumn(
                name: "ExpenseSummary",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Shops",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DomainUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseSummary = table.Column<decimal>(type: "numeric", nullable: false),
                    IdentityId = table.Column<Guid>(type: "uuid", nullable: false),
                    imagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainUser", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_DomainUser_OwnerId",
                table: "Shops",
                column: "OwnerId",
                principalTable: "DomainUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSellers_DomainUser_SellerId",
                table: "ShopSellers",
                column: "SellerId",
                principalTable: "DomainUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
