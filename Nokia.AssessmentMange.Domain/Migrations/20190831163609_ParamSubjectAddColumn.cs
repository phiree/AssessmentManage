using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    public partial class ParamSubjectAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PSubjectName",
                table: "ParamSubject",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PSubjectName",
                table: "ParamSubject");
        }
    }
}
