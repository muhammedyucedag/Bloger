﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloger.DataAccess.Migrations
{
    public partial class usermewcolumnroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");
        }
    }
}
