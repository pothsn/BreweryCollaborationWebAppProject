using Microsoft.EntityFrameworkCore.Migrations;

namespace BreweryCollaborationWebbApp.Migrations
{
    public partial class SeedBeerStyles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (1, 'Ale')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (2, 'Lager')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (3, 'India Pale Ale')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (4, 'Stout')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (5, 'Pale ale')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (6, 'Wheat beer')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (7, 'Pilsner')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (8, 'Porter')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (9, 'Sour')");
            migrationBuilder.Sql("SET Identity_Insert BeerStyle ON INSERT INTO BeerStyle (Id, Name) VALUES (10, 'Saison')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
