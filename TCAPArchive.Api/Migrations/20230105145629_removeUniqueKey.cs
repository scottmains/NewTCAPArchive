using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class removeUniqueKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatLines_Position",
                table: "ChatLines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_Position",
                table: "ChatLines",
                column: "Position",
                unique: true);
        }
    }
}
