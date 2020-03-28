using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotCore.Migrations
{
    public partial class created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    ChatUId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    InstaUserName = table.Column<string>(nullable: true),
                    UserStatus = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserUid = table.Column<string>(nullable: true),
                    ChatId = table.Column<int>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    TelegramUserId = table.Column<int>(nullable: false),
                    UnknownChatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.CheckConstraint("CK_Users_UserStatus_Enum_Constraint", "[UserStatus] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatMessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    SenderChatId = table.Column<int>(nullable: false),
                    SenderName = table.Column<string>(nullable: true),
                    ReceiverChatId = table.Column<int>(nullable: false),
                    ReceiverName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ChatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannalJioneds",
                columns: table => new
                {
                    JionedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    ChannalId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannalJioneds", x => x.JionedId);
                    table.ForeignKey(
                        name: "FK_ChannalJioneds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crashes",
                columns: table => new
                {
                    CrashId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MainUserName = table.Column<string>(nullable: true),
                    CrashUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crashes", x => x.CrashId);
                    table.ForeignKey(
                        name: "FK_Crashes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    UserChatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    RemoveTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ChatId = table.Column<int>(nullable: false),
                    UserTelegramId = table.Column<int>(nullable: false),
                    UserTelegramChatId = table.Column<int>(nullable: false),
                    InstaUserName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.UserChatId);
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannalJioneds_UserId",
                table: "ChannalJioneds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Crashes_UserId",
                table: "Crashes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatId",
                table: "UserChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_UserId",
                table: "UserChats",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannalJioneds");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Crashes");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
