using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Migrations
{
    public partial class stingLocationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StingLocation",
                table: "Predators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StingLocation",
                table: "Decoys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StingLocation",
                table: "Predators");

            migrationBuilder.DropColumn(
                name: "StingLocation",
                table: "Decoys");
        }
    }
}
