using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaylistService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "playlists_schema");

            migrationBuilder.CreateTable(
                name: "playlists",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    playlist_type = table.Column<int>(type: "integer", nullable: false),
                    access_type = table.Column<int>(type: "integer", nullable: false),
                    owner = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "song_text",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    approved = table.Column<bool>(type: "boolean", nullable: false),
                    text_author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    song_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song_text", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SongAuthors",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    song_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAuthors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "playlist_authors",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlist_authors", x => x.id);
                    table.ForeignKey(
                        name: "FK_playlist_authors_playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalSchema: "playlists_schema",
                        principalTable: "playlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "song",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    OriginalAlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    published_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    @explicit = table.Column<bool>(name: "explicit", type: "boolean", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    cover_url = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song", x => x.id);
                    table.ForeignKey(
                        name: "FK_song_playlists_OriginalAlbumId",
                        column: x => x.OriginalAlbumId,
                        principalSchema: "playlists_schema",
                        principalTable: "playlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_playlist_authors_PlaylistId",
                schema: "playlists_schema",
                table: "playlist_authors",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_song_OriginalAlbumId",
                schema: "playlists_schema",
                table: "song",
                column: "OriginalAlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playlist_authors",
                schema: "playlists_schema");

            migrationBuilder.DropTable(
                name: "song",
                schema: "playlists_schema");

            migrationBuilder.DropTable(
                name: "song_text",
                schema: "playlists_schema");

            migrationBuilder.DropTable(
                name: "SongAuthors",
                schema: "playlists_schema");

            migrationBuilder.DropTable(
                name: "playlists",
                schema: "playlists_schema");
        }
    }
}
