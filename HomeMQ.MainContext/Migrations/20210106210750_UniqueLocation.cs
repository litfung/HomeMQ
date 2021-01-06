using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeMQ.MainContext.Migrations
{
    public partial class UniqueLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationName",
                table: "Locations",
                column: "LocationName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_LocationName",
                table: "Locations");
        }
    }
}
