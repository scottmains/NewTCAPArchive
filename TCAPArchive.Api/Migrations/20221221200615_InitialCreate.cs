using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Decoys_DecoyId",
                table: "ChatSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Predators_PredatorId",
                table: "ChatSessions");

            migrationBuilder.DropIndex(
                name: "IX_ChatSessions_DecoyId",
                table: "ChatSessions");

            migrationBuilder.DropIndex(
                name: "IX_ChatSessions_PredatorId",
                table: "ChatSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_DecoyId",
                table: "ChatSessions",
                column: "DecoyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_PredatorId",
                table: "ChatSessions",
                column: "PredatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Decoys_DecoyId",
                table: "ChatSessions",
                column: "DecoyId",
                principalTable: "Decoys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Predators_PredatorId",
                table: "ChatSessions",
                column: "PredatorId",
                principalTable: "Predators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
