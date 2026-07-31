using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTableNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_members_UserProfile_MemberId",
                schema: "social_schema",
                table: "chat_members");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_messages_UserProfile_AuthorId",
                schema: "social_schema",
                table: "chat_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                schema: "social_schema",
                table: "UserProfile");

            migrationBuilder.RenameTable(
                name: "UserProfile",
                schema: "social_schema",
                newName: "user_profile",
                newSchema: "social_schema");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfile_user_tag",
                schema: "social_schema",
                table: "user_profile",
                newName: "IX_user_profile_user_tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profile",
                schema: "social_schema",
                table: "user_profile",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_members_user_profile_MemberId",
                schema: "social_schema",
                table: "chat_members",
                column: "MemberId",
                principalSchema: "social_schema",
                principalTable: "user_profile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_messages_user_profile_AuthorId",
                schema: "social_schema",
                table: "chat_messages",
                column: "AuthorId",
                principalSchema: "social_schema",
                principalTable: "user_profile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_members_user_profile_MemberId",
                schema: "social_schema",
                table: "chat_members");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_messages_user_profile_AuthorId",
                schema: "social_schema",
                table: "chat_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profile",
                schema: "social_schema",
                table: "user_profile");

            migrationBuilder.RenameTable(
                name: "user_profile",
                schema: "social_schema",
                newName: "UserProfile",
                newSchema: "social_schema");

            migrationBuilder.RenameIndex(
                name: "IX_user_profile_user_tag",
                schema: "social_schema",
                table: "UserProfile",
                newName: "IX_UserProfile_user_tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                schema: "social_schema",
                table: "UserProfile",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_members_UserProfile_MemberId",
                schema: "social_schema",
                table: "chat_members",
                column: "MemberId",
                principalSchema: "social_schema",
                principalTable: "UserProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_messages_UserProfile_AuthorId",
                schema: "social_schema",
                table: "chat_messages",
                column: "AuthorId",
                principalSchema: "social_schema",
                principalTable: "UserProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
