using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloger.DataAccess.Migrations
{
    public partial class AddOrderByCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Category");
        }
    }
}
