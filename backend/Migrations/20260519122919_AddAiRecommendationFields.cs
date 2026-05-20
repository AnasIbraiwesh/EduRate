using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAiRecommendationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetLevel",
                table: "Universities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Levels",
                table: "Universities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Majors",
                table: "Universities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Ranking",
                table: "Universities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Professors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Professors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TeachingStyle",
                table: "Professors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetLevel",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Levels",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Majors",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Ranking",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "TeachingStyle",
                table: "Professors");
        }
    }
}
