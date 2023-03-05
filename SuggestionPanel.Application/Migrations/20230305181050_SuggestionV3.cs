using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionPanel.Application.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Suggestions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Suggestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "Suggestions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Suggestions");
        }
    }
}
