using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    [Migration("20260530160000_RemoveAsuAndFixRankings")]
    public partial class RemoveAsuAndFixRankings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Soft-delete ASU's professors first
            migrationBuilder.Sql(
                "UPDATE \"Professors\" SET \"IsDeleted\" = true " +
                "WHERE \"UniversityId\" = (" +
                "  SELECT \"UniversityId\" FROM \"Universities\" " +
                "  WHERE \"Name\" = 'Applied Science Private University' LIMIT 1);");

            // Soft-delete ASU university
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"IsDeleted\" = true " +
                "WHERE \"Name\" = 'Applied Science Private University';");

            // Fix rankings: shift 9→8 and 10→9
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 8 " +
                "WHERE \"Name\" = 'Al-Ahliyya Amman University' AND \"IsDeleted\" = false;");
            migrationBuilder.Sql(
                "UPDATE \"Universities\" SET \"Ranking\" = 9 " +
                "WHERE \"Name\" = 'Al-Balqa Applied University' AND \"IsDeleted\" = false;");
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
