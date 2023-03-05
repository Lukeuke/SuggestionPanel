using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionPanel.Application.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "Suggestions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ImplementationDate",
                table: "Suggestions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementationDesc",
                table: "Suggestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PropositionDate",
                table: "Suggestions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delete",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "ImplementationDate",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "ImplementationDesc",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "PropositionDate",
                table: "Suggestions");
        }
    }
}
