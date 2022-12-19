using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class updatedchatsessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PredatorId",
                table: "Decoys");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "ChatLines");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "ChatLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "ChatLines");

            migrationBuilder.AddColumn<Guid>(
                name: "PredatorId",
                table: "Decoys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "ChatLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
