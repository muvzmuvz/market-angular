using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace_api.Migrations
{
    /// <inheritdoc />
    public partial class AddShopSellersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopSeller_DomainUser_SellerId",
                table: "ShopSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSeller_Shops_ShopId",
                table: "ShopSeller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopSeller",
                table: "ShopSeller");

            migrationBuilder.RenameTable(
                name: "ShopSeller",
                newName: "ShopSellers");

            migrationBuilder.RenameIndex(
                name: "IX_ShopSeller_ShopId",
                table: "ShopSellers",
                newName: "IX_ShopSellers_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopSeller_SellerId",
                table: "ShopSellers",
                newName: "IX_ShopSellers_SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopSellers",
                table: "ShopSellers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSellers_DomainUser_SellerId",
                table: "ShopSellers",
                column: "SellerId",
                principalTable: "DomainUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSellers_Shops_ShopId",
                table: "ShopSellers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopSellers_DomainUser_SellerId",
                table: "ShopSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSellers_Shops_ShopId",
                table: "ShopSellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopSellers",
                table: "ShopSellers");

            migrationBuilder.RenameTable(
                name: "ShopSellers",
                newName: "ShopSeller");

            migrationBuilder.RenameIndex(
                name: "IX_ShopSellers_ShopId",
                table: "ShopSeller",
                newName: "IX_ShopSeller_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopSellers_SellerId",
                table: "ShopSeller",
                newName: "IX_ShopSeller_SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopSeller",
                table: "ShopSeller",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSeller_DomainUser_SellerId",
                table: "ShopSeller",
                column: "SellerId",
                principalTable: "DomainUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSeller_Shops_ShopId",
                table: "ShopSeller",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
