using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserMigrationsRefactory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "_createAt",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_createAt",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "MyProperty",
                table: "User",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
