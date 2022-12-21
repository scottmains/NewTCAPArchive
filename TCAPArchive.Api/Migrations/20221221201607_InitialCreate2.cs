using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatSessionId",
                table: "ChatLines");

            migrationBuilder.DropIndex(
                name: "IX_ChatLines_ChatSessionId",
                table: "ChatLines");

            migrationBuilder.DropColumn(
                name: "ChatSessionId",
                table: "ChatLines");

            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_ChatId",
                table: "ChatLines",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatId",
                table: "ChatLines",
                column: "ChatId",
                principalTable: "ChatSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatId",
                table: "ChatLines");

            migrationBuilder.DropIndex(
                name: "IX_ChatLines_ChatId",
                table: "ChatLines");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatSessionId",
                table: "ChatLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_ChatSessionId",
                table: "ChatLines",
                column: "ChatSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatLines_ChatSessions_ChatSessionId",
                table: "ChatLines",
                column: "ChatSessionId",
                principalTable: "ChatSessions",
                principalColumn: "Id");
        }
    }
}
