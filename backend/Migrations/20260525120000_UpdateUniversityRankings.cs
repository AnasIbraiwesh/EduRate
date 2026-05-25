using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduRateSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUniversityRankings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 1 WHERE ""Name"" = 'Al Hussein Technical University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 2 WHERE ""Name"" = 'The University of Jordan';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 3 WHERE ""Name"" = 'Princess Sumaya University for Technology';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 4 WHERE ""Name"" = 'Jordan University of Science and Technology';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 5 WHERE ""Name"" = 'Yarmouk University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 6 WHERE ""Name"" = 'German Jordanian University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 7 WHERE ""Name"" = 'American University of Madaba';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 8 WHERE ""Name"" = 'Applied Science Private University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 9 WHERE ""Name"" = 'Al-Ahliyya Amman University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 10 WHERE ""Name"" = 'Al-Balqa Applied University';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 10 WHERE ""Name"" = 'Al Hussein Technical University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 1 WHERE ""Name"" = 'The University of Jordan';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 3 WHERE ""Name"" = 'Princess Sumaya University for Technology';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 2 WHERE ""Name"" = 'Jordan University of Science and Technology';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 4 WHERE ""Name"" = 'Yarmouk University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 5 WHERE ""Name"" = 'German Jordanian University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 6 WHERE ""Name"" = 'American University of Madaba';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 7 WHERE ""Name"" = 'Applied Science Private University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 8 WHERE ""Name"" = 'Al-Ahliyya Amman University';");
            migrationBuilder.Sql(@"UPDATE ""Universities"" SET ""Ranking"" = 9 WHERE ""Name"" = 'Al-Balqa Applied University';");
        }
    }
}
