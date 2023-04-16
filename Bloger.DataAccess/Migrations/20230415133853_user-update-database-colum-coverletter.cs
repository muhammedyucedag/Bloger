using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloger.DataAccess.Migrations
{
    public partial class userupdatedatabasecolumcoverletter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "AspNetUsers");
        }
    }
}
