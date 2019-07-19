using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class derrrr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "CollaborationRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "CollaborationRequest");
        }
    }
}
