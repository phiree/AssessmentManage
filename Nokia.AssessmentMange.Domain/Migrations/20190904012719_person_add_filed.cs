using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    public partial class person_add_filed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "IdNo",
               table: "Person",
               nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilitaryRank",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "IdNo",
              table: "Person");

            migrationBuilder.DropColumn(
                name: "MilitaryRank",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Person");
        }
    }
}
