using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSentimentToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sentiment",
                table: "UniversityReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Professors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sentiment",
                table: "ProfessorReviews",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "UniversityReviews");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "ProfessorReviews");
        }
    }
}
