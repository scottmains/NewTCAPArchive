using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedNameToChatSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChatSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChatSessions");
        }
    }
}
