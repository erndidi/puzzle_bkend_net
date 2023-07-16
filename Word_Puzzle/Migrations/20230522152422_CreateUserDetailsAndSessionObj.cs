using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPuzzle.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserDetailsAndSessionObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.CreateTable(
          name: "UserDetails",
          columns: table => new
          {
              Id = table.Column<Guid>(nullable: false),
              Email = table.Column<string>(nullable: true),
              FirstName = table.Column<string>(nullable: true),
              LastName = table.Column<string>(nullable: true),
              GoogleId = table.Column<string>(nullable: true),
              FacebookId = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
              table.PrimaryKey("PK_UserDetails", x => x.Id);
          });
          */

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
            /*
            migrationBuilder.CreateTable(
               name: "UserWords",
               columns: table => new
               {
                   Id = table.Column<Guid>(nullable: false),
                   UsedWords = table.Column<string>(nullable: true),
                   UserId = table.Column<Guid>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserWords", x => x.Id);
                   table.ForeignKey(
                       name: "FK_UserWords_UserDetails_UserId",
                       column: x => x.UserId,
                       principalTable: "UserDetails",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
               });
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
