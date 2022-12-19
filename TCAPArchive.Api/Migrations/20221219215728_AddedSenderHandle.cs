using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCAPArchive.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedSenderHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderHandle",
                table: "ChatLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderHandle",
                table: "ChatLines");
        }
    }
}
