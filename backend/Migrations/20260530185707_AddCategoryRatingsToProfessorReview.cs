using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRatingsToProfessorReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Friendliness",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Workload",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Backfill legacy reviews (which only stored a single composite Rating) so the new
            // per-category columns are populated with a sensible 1-5 value instead of 0.
            migrationBuilder.Sql(
                "UPDATE \"ProfessorReviews\" SET \"Difficulty\" = \"Rating\", \"Workload\" = \"Rating\", \"Friendliness\" = \"Rating\";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "Friendliness",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "Workload",
                table: "ProfessorReviews");
        }
    }
}
