using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameTracker.Forser.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_GameInfo_GameId",
                        column: x => x.GameId,
                        principalTable: "GameInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "GameInfo",
                columns: new[] { "Id", "GameDescription", "GameTitle", "SessionId" },
                values: new object[,]
                {
                    { 1, "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second.", "Grand Theft Auto V", null },
                    { 2, "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamor, and ceaseless body modification.", "CyberPunk 2077", null },
                    { 3, "Honkai: Star Rail is a new HoYoverse space fantasy RPG. Hop aboard the Astral Express and experience the galaxy's infinite wonders on this journey filled with adventure and thrills.", "Honkai: Star Rail", null }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "GameId", "SessionEnd", "SessionStart" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2023, 10, 13, 18, 57, 49, 656, DateTimeKind.Local).AddTicks(696), new DateTime(2023, 10, 13, 16, 57, 49, 656, DateTimeKind.Local).AddTicks(597) },
                    { 2, 2, new DateTime(2023, 10, 13, 17, 57, 49, 656, DateTimeKind.Local).AddTicks(704), new DateTime(2023, 10, 12, 16, 57, 49, 656, DateTimeKind.Local).AddTicks(702) },
                    { 3, 1, new DateTime(2023, 10, 14, 0, 57, 49, 656, DateTimeKind.Local).AddTicks(707), new DateTime(2023, 10, 13, 20, 57, 49, 656, DateTimeKind.Local).AddTicks(706) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GameId",
                table: "Sessions",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "GameInfo");
        }
    }
}
