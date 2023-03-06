using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionPanel.Application.Migrations
{
    /// <inheritdoc />
    public partial class ToCommittee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ToCommittee",
                table: "Suggestions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToCommittee",
                table: "Suggestions");
        }
    }
}
