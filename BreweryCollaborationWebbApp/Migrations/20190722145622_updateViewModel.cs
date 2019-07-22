using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class updateViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Today",
                table: "Collaboration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Today",
                table: "Collaboration",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
