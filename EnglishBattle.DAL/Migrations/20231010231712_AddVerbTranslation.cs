using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishBattle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddVerbTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "IrregularVerbs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Translation",
                table: "IrregularVerbs");
        }
    }
}
