using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishBattle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IrregularVerbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BaseForm = table.Column<string>(type: "TEXT", nullable: false),
                    PastParticiple = table.Column<string>(type: "TEXT", nullable: false),
                    PastSimple = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrregularVerbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Seconds = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OverAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameAnswers",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    VerbId = table.Column<int>(type: "INTEGER", nullable: false),
                    PastParticipleInput = table.Column<string>(type: "TEXT", nullable: false),
                    PastSimpleInput = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimePenalty = table.Column<int>(type: "INTEGER", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAnswers", x => new { x.GameId, x.VerbId });
                    table.ForeignKey(
                        name: "FK_GameAnswers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameAnswers_IrregularVerbs_VerbId",
                        column: x => x.VerbId,
                        principalTable: "IrregularVerbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameAnswers_VerbId",
                table: "GameAnswers",
                column: "VerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameAnswers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "IrregularVerbs");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
