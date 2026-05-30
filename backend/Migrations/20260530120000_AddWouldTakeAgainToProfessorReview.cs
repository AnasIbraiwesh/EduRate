using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using eduRateSystem.Data;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260530120000_AddWouldTakeAgainToProfessorReview")]
    public partial class AddWouldTakeAgainToProfessorReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"ProfessorReviews\" ADD COLUMN IF NOT EXISTS \"WouldTakeAgain\" boolean;");
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
