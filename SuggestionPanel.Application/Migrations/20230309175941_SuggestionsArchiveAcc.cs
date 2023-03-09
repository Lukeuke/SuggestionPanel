using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionPanel.Application.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionsArchiveAcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Suggestions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Archive",
                table: "Suggestions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "Archive",
                table: "Suggestions");
        }
    }
}
