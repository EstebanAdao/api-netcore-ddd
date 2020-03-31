using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserMigrationsRefactory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_createAt",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "_createAt",
                table: "User",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
