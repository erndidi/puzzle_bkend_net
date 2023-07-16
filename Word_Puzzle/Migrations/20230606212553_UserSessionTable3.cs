using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPuzzle.Migrations
{
    /// <inheritdoc />
    public partial class UserSessionTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "UserSessions",
           columns: table => new
           {
               Id = table.Column<Guid>(nullable: false),
               SessionId = table.Column<string>(nullable: true),
               UserId = table.Column<Guid>(nullable: true),
               DateEnteredTIme = table.Column<DateTime>(nullable: true)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_UserSessions", x => x.Id);
               table.ForeignKey(
                   name: "FK_UserSessions_UserDetails_UserId",
                   column: x => x.UserId,
                   principalTable: "UserDetails",
                   principalColumn: "Id",
                   onDelete: ReferentialAction.Restrict);
           });

            migrationBuilder.AlterColumn<string>(
        name: "SessionId",
        table: "UserSessions",
        type: "nvarchar(128)", // Specify the new column type with the desired length
        maxLength: 128,        // Specify the maximum length as 128
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true);

        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
