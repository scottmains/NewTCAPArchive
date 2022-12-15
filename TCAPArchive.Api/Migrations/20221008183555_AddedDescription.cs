using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Migrations
{
    public partial class AddedDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Predators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Decoys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Predators");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Decoys");
        }
    }
}
