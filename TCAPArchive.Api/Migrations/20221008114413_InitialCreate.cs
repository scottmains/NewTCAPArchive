using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            

            migrationBuilder.CreateTable(
                name: "Decoys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Handle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decoys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Handle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PredatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DecoyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatLines_Decoys_DecoyId",
                        column: x => x.DecoyId,
                        principalTable: "Decoys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatLines_Predators_PredatorId",
                        column: x => x.PredatorId,
                        principalTable: "Predators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_DecoyId",
                table: "ChatLines",
                column: "DecoyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatLines_PredatorId",
                table: "ChatLines",
                column: "PredatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatLines");

            migrationBuilder.DropTable(
                name: "Decoys");

            migrationBuilder.DropTable(
                name: "Predators");
        }
    }
}
