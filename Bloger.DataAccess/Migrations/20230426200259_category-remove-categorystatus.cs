using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloger.DataAccess.Migrations
{
    public partial class categoryremovecategorystatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryStatus",
                table: "Category",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Category",
                newName: "CategoryStatus");
        }
    }
}
