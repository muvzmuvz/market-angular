using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPasportForShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "SiteConfigurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageOwnerPath",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportOwner",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "SiteConfigurations");

            migrationBuilder.DropColumn(
                name: "INN",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ImageOwnerPath",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "PassportOwner",
                table: "Shops");
        }
    }
}
