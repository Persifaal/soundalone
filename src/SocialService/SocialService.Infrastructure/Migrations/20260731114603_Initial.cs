using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "social_schema");

            migrationBuilder.CreateTable(
                name: "chat_message_attachments",
                schema: "social_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    attachment_playlist_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_message_attachments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                schema: "social_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_type = table.Column<string>(type: "text", nullable: false),
                    chat_name = table.Column<string>(type: "text", nullable: true),
                    chat_icon_url = table.Column<string>(type: "text", nullable: true),
                    chat_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "social_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_tag = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    avatar_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    banner_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_musician = table.Column<bool>(type: "boolean", nullable: false),
                    musician_type = table.Column<int>(type: "integer", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    related_account = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_members",
                schema: "social_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_members_UserProfile_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "social_schema",
                        principalTable: "UserProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_members_chats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "social_schema",
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_messages",
                schema: "social_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    message_content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_messages_UserProfile_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "social_schema",
                        principalTable: "UserProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chat_members_ChatId",
                schema: "social_schema",
                table: "chat_members",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_chat_members_MemberId",
                schema: "social_schema",
                table: "chat_members",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_AuthorId",
                schema: "social_schema",
                table: "chat_messages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_user_tag",
                schema: "social_schema",
                table: "UserProfile",
                column: "user_tag",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_members",
                schema: "social_schema");

            migrationBuilder.DropTable(
                name: "chat_message_attachments",
                schema: "social_schema");

            migrationBuilder.DropTable(
                name: "chat_messages",
                schema: "social_schema");

            migrationBuilder.DropTable(
                name: "chats",
                schema: "social_schema");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "social_schema");
        }
    }
}
