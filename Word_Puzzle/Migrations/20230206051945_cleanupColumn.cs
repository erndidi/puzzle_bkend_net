using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPuzzle.Migrations
{
    /// <inheritdoc />
    public partial class cleanupColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Def",
            //    table: "Definitions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Def",
                table: "Definitions",
                type: "nvarchar",
                nullable: false,
                defaultValue: "");
        }
    }
}
