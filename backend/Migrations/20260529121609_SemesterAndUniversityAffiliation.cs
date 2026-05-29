using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class SemesterAndUniversityAffiliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniversityReviews_UserId_UniversityId",
                table: "UniversityReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviews_UserId_ProfessorId",
                table: "ProfessorReviews");

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "UniversityReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "ProfessorReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniversityChangedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniversityReviews_UserId_UniversityId_Semester",
                table: "UniversityReviews",
                columns: new[] { "UserId", "UniversityId", "Semester" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_UserId_ProfessorId_Semester",
                table: "ProfessorReviews",
                columns: new[] { "UserId", "ProfessorId", "Semester" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniversityReviews_UserId_UniversityId_Semester",
                table: "UniversityReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviews_UserId_ProfessorId_Semester",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "UniversityReviews");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "UniversityChangedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityReviews_UserId_UniversityId",
                table: "UniversityReviews",
                columns: new[] { "UserId", "UniversityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_UserId_ProfessorId",
                table: "ProfessorReviews",
                columns: new[] { "UserId", "ProfessorId" },
                unique: true);
        }
    }
}
