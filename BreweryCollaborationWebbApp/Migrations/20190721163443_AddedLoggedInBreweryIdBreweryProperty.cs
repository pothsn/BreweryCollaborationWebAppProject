using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class AddedLoggedInBreweryIdBreweryProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoggedInBreweryId",
                table: "Brewery",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedInBreweryId",
                table: "Brewery");
        }
    }
}
