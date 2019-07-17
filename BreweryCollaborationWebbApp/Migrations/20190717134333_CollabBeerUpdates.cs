using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class CollabBeerUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrewSite",
                table: "Collaboration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrewSite",
                table: "Collaboration");
        }
    }
}
