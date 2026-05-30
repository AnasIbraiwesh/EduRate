using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    [Migration("20260530140000_PatchAsuImageUrl")]
    public partial class PatchAsuImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Universities\" " +
                "SET \"ImageUrl\" = 'https://www.unirank.org/i/logos-seals/applied-science-private-university-jo-logo-seal.png' " +
                "WHERE \"Name\" = 'Applied Science Private University' AND (\"ImageUrl\" IS NULL OR \"ImageUrl\" = '');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Universities\" " +
                "SET \"ImageUrl\" = NULL " +
                "WHERE \"Name\" = 'Applied Science Private University';");
        }
    }
}
