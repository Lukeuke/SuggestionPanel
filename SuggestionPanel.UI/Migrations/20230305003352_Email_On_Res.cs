using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionPanel.UI.Migrations
{
    /// <inheritdoc />
    public partial class Email_On_Res : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ValueStreamResponsibilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ValueStreamResponsibilities");
        }
    }
}
