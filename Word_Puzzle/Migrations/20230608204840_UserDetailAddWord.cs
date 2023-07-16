using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPuzzle.Migrations
{
    /// <inheritdoc />
    public partial class UserDetailAddWord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
        name: "UserWords",
        table: "UserDetails",
        nullable: true);

        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSessions_UserDetailId",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserDetail");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserDetailId",
                table: "UserSessions",
                column: "UserDetailId");
        }
    }
}
