using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    [Migration("20260530120000_AddWouldTakeAgainToProfessorReview")]
    public partial class AddWouldTakeAgainToProfessorReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WouldTakeAgain",
                table: "ProfessorReviews",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WouldTakeAgain",
                table: "ProfessorReviews");
        }
    }
}
