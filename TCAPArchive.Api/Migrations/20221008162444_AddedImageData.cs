using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Migrations
{
    public partial class AddedImageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Predators",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Predators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Decoys",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Decoys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Predators");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Predators");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Decoys");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Decoys");
        }
    }
}
