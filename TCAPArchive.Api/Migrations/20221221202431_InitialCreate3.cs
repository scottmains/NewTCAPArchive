using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatId",
                table: "ChatLines");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatLines",
                newName: "ChatSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatLines_ChatId",
                table: "ChatLines",
                newName: "IX_ChatLines_ChatSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatSessionId",
                table: "ChatLines",
                column: "ChatSessionId",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatSessionId",
                table: "ChatLines");

            migrationBuilder.RenameColumn(
                name: "ChatSessionId",
                table: "ChatLines",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatLines_ChatSessionId",
                table: "ChatLines",
                newName: "IX_ChatLines_ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatId",
                table: "ChatLines",
                column: "ChatId",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
