using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestionsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedAsDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "UsedAsDaily",
                table: "Questions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedAsDaily",
                table: "Questions");
        }
    }
}
