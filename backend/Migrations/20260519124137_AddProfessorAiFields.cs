using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessorAiFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Professors",
                newName: "CoursesJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoursesJson",
                table: "Professors",
                newName: "Course");
        }
    }
}
