using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecom_120.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class checkboxadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "ShopingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "ShopingCarts");
        }
    }
}
