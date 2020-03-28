using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotCore.Migrations
{
    public partial class editUserA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_UserStatus_Enum_Constraint",
                table: "Users");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Users_UserStatus_Enum_Constraint",
                table: "Users",
                sql: "[UserStatus] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)");

            migrationBuilder.AddColumn<string>(
                name: "InstaProfileImage",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstaUserId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_UserStatus_Enum_Constraint",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InstaProfileImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InstaUserId",
                table: "Users");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Users_UserStatus_Enum_Constraint",
                table: "Users",
                sql: "[UserStatus] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
        }
    }
}
