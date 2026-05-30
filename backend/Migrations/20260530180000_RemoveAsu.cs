using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using eduRateSystem.Data;

#nullable disable

namespace eduRateSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260530180000_RemoveAsu")]
    public partial class RemoveAsu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Soft-delete ASU's professors
            migrationBuilder.Sql(
                "UPDATE \"Professors\" SET \"IsDeleted\" = true " +
                "WHERE \"UniversityId\" = (" +
                "  SELECT \"UniversityId\" FROM \"Universities\" " +
                "  WHERE \"Name\" = 'Applied Science Private University' LIMIT 1);");

            // Soft-delete ASU university
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"IsDeleted\" = true " +
                "WHERE \"Name\" = 'Applied Science Private University';");

            // Shift rankings to close the gap: 9->8, 10->9
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 8 " +
                "WHERE \"Name\" = 'Al-Ahliyya Amman University';");
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 9 " +
                "WHERE \"Name\" = 'Al-Balqa Applied University';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 10 " +
                "WHERE \"Name\" = 'Al-Balqa Applied University';");
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 9 " +
                "WHERE \"Name\" = 'Al-Ahliyya Amman University';");
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"IsDeleted\" = false " +
                "WHERE \"Name\" = 'Applied Science Private University';");
            migrationBuilder.Sql(
                "UPDATE \"Professors\" SET \"IsDeleted\" = false " +
                "WHERE \"UniversityId\" = (" +
                "  SELECT \"UniversityId\" FROM \"Universities\" " +
                "  WHERE \"Name\" = 'Applied Science Private University' LIMIT 1);");
        }
    }
}
