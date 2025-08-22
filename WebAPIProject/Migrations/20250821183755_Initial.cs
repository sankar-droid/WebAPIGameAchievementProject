using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPIProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    GameGenreId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Popularity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => x.GameGenreId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GameGenreId = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_GameGenres_GameGenreId",
                        column: x => x.GameGenreId,
                        principalTable: "GameGenres",
                        principalColumn: "GameGenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AchievementName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Badge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievements_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameGenreId", "Description", "GenreName", "Popularity" },
                values: new object[,]
                {
                    { "GEN01", "Dark and terrifying survival horror experiences", "Horror", "High" },
                    { "GEN02", "Games focusing on avoiding detection", "Stealth", "Medium" },
                    { "GEN03", "Narrative-rich deep plots", "Story-Driven", "Very High" },
                    { "GEN04", "Japanese Samurai and Bushido-inspired action", "Samurai", "High" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "sankar@gmail.com", "Sankar", "1234567", "Moderator" },
                    { 2, "mahee@gmail.com", "Maheedhar", "345781", "Player" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Description", "Developer", "GameGenreId", "GameName", "ReleaseYear" },
                values: new object[,]
                {
                    { "GAME01", "Survival horror in a cursed village", "Capcom", "GEN01", "Resident Evil Village", "2021" },
                    { "GAME02", "Psychological horror classic", "Team Silent", "GEN01", "Silent Hill 2", "2001" }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "AchievementId", "AchievementName", "Badge", "Difficulty", "GameId", "Reward" },
                values: new object[,]
                {
                    { "ACH01", "Village Survivor", "Bronze", "Medium", "GAME01", "Ammo Pack" },
                    { "ACH02", "Monster Hunter", "Silver", "Hard", "GAME01", "Weapon Upgrade" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AchievementName",
                table: "Achievements",
                column: "AchievementName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_GameId",
                table: "Achievements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreName",
                table: "GameGenres",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameGenreId",
                table: "Games",
                column: "GameGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameName",
                table: "Games",
                column: "GameName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameGenres");
        }
    }
}
