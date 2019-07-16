using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class BreweryImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Brewery",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Brewery",
                newName: "Logo");
        }
    }
}
