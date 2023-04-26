using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloger.DataAccess.Migrations
{
    public partial class blogremoveblogstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogStatus",
                table: "Blog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BlogStatus",
                table: "Blog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
