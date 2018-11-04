using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maple.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mediaplayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "GetDate()")
                        .Annotation("Sqlite:Autoincrement", true),
                    Sequence = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    PlaylistId = table.Column<int>(nullable: false),
                    Playlist = table.Column<string>(type: "PlaylistModel", nullable: false),
                    DeviceName = table.Column<string>(maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediaplayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sequence = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    Value = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sequence = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Location = table.Column<string>(nullable: true),
                    IsShuffeling = table.Column<bool>(nullable: false),
                    PrivacyStatus = table.Column<int>(nullable: false),
                    RepeatMode = table.Column<int>(nullable: false),
                    MediaPlayerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Mediaplayers_MediaPlayerId",
                        column: x => x.MediaPlayerId,
                        principalTable: "Mediaplayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "GetDate()")
                        .Annotation("Sqlite:Autoincrement", true),
                    Sequence = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true, defaultValue: "SYSTEM"),
                    PlaylistId = table.Column<int>(nullable: false),
                    Duration = table.Column<long>(nullable: false),
                    PrivacyStatus = table.Column<int>(nullable: false),
                    MediaItemType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Location = table.Column<string>(maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaItems_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedPlaylist", 0, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedMediaItem", 10, 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedMediaPlayer", 20, 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedPrimary", 40, 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedAccent", 50, 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Key", "Sequence", "Type", "UpdatedBy", "UpdatedOn", "Value" },
                values: new object[] { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SelectedScene", 60, 16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "IsShuffeling", "Location", "MediaPlayerId", "PrivacyStatus", "RepeatMode", "Sequence", "Title", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test playlist with mp3 files", false, "https://www.youtube.com/watch?v=WxfcsmbBd00&t=0s", 1, 0, 1, 0, "MP3 Files", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_PlaylistId",
                table: "MediaItems",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_MediaPlayerId",
                table: "Playlists",
                column: "MediaPlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Mediaplayers");
        }
    }
}
