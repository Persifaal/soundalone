using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaylistService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSongParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "playlist_song_participants",
                schema: "playlists_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    playlist_id = table.Column<Guid>(type: "uuid", nullable: false),
                    song_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlist_song_participants", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playlist_song_participants",
                schema: "playlists_schema");
        }
    }
}
