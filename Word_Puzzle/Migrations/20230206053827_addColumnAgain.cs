using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPuzzle.Migrations
{
    /// <inheritdoc />
    public partial class addColumnAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Def",
                table: "Definitions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Def",
                table: "Definitions");
        }
    }
}
