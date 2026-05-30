using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    [Migration("20260530150000_FixAsuImageUrl")]
    public partial class FixAsuImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Universities\" " +
                "SET \"ImageUrl\" = 'https://www.asu.edu.jo/PublishingImages/logo-02.png' " +
                "WHERE \"Name\" = 'Applied Science Private University';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Universities\" " +
                "SET \"ImageUrl\" = 'https://www.unirank.org/i/logos-seals/applied-science-private-university-jo-logo-seal.png' " +
                "WHERE \"Name\" = 'Applied Science Private University';");
        }
    }
}
