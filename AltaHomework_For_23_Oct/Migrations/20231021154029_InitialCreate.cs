using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaHomework_For_23_Oct.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    User1Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    User2Guid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.User1Guid);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_User1Guid",
                        column: x => x.User1Guid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_User2Guid",
                        column: x => x.User2Guid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    SenderGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecipientGuid",
                        column: x => x.RecipientGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderGuid",
                        column: x => x.SenderGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersFriends",
                columns: table => new
                {
                    User1Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    User2Guid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFriends", x => x.User1Guid);
                    table.ForeignKey(
                        name: "FK_UsersFriends_Users_User1Guid",
                        column: x => x.User1Guid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersFriends_Users_User2Guid",
                        column: x => x.User2Guid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });
            
            // Создаёт функциональный индекс
            migrationBuilder.Sql(@"CREATE UNIQUE INDEX IX_Users_UserName ON ""Users"" (lower(""UserName""));");
            
            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_User2Guid",
                table: "FriendRequests",
                column: "User2Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientGuid",
                table: "Messages",
                column: "RecipientGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderGuid",
                table: "Messages",
                column: "SenderGuid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriends_User2Guid",
                table: "UsersFriends",
                column: "User2Guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UsersFriends");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
