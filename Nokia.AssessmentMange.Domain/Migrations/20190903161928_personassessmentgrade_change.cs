using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    public partial class personassessmentgrade_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSubject_Subjects_PSubjectId",
                table: "ParamSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrade_PersonGrades_PersonAssessmentGradeId",
                table: "SubjectGrade");

            migrationBuilder.DropPrimaryKey(
                name: "SubjectGradeId",
                table: "SubjectGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamSubject",
                table: "ParamSubject");

            migrationBuilder.DropColumn(
                name: "IsAbsent",
                table: "PersonGrades");

            migrationBuilder.DropColumn(
                name: "IsMakeup",
                table: "PersonGrades");

            migrationBuilder.RenameColumn(
                name: "FloorAge",
                table: "ConversionCell",
                newName: "FloorAgeAsKey");

            migrationBuilder.AddColumn<short>(
                name: "IsMakeup",
                table: "SubjectGrade",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "SubjectConversion",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "PSubjectId",
                table: "ParamSubject",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "ConversionCell",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "ConversionCell",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PMS",
                table: "SubjectGrade",
                columns: new[] { "PersonAssessmentGradeId", "IsMakeup", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamSubject",
                table: "ParamSubject",
                columns: new[] { "SubjectId", "SortOrder", "PSubjectId" });

            migrationBuilder.CreateTable(
                name: "AssessmentGrade",
                columns: table => new
                {
                    IsMakeup = table.Column<short>(nullable: false),
                    PersonAssessmentGradeId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    IsAbsent = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssessmentGradeId", x => new { x.PersonAssessmentGradeId, x.IsMakeup });
                    table.ForeignKey(
                        name: "FK_AssessmentGrade_PersonGrades_PersonAssessmentGradeId",
                        column: x => x.PersonAssessmentGradeId,
                        principalTable: "PersonGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSubject_Subjects_PSubjectId",
                table: "ParamSubject",
                column: "PSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrade_AssessmentGrade_PersonAssessmentGradeId_IsMakeup",
                table: "SubjectGrade",
                columns: new[] { "PersonAssessmentGradeId", "IsMakeup" },
                principalTable: "AssessmentGrade",
                principalColumns: new[] { "PersonAssessmentGradeId", "IsMakeup" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSubject_Subjects_PSubjectId",
                table: "ParamSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrade_AssessmentGrade_PersonAssessmentGradeId_IsMakeup",
                table: "SubjectGrade");

            migrationBuilder.DropTable(
                name: "AssessmentGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PMS",
                table: "SubjectGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamSubject",
                table: "ParamSubject");

            migrationBuilder.DropColumn(
                name: "IsMakeup",
                table: "SubjectGrade");

            migrationBuilder.RenameColumn(
                name: "FloorAgeAsKey",
                table: "ConversionCell",
                newName: "FloorAge");

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "SubjectConversion",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<short>(
                name: "IsAbsent",
                table: "PersonGrades",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "IsMakeup",
                table: "PersonGrades",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "PSubjectId",
                table: "ParamSubject",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "ConversionCell",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "ConversionCell",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "SubjectGradeId",
                table: "SubjectGrade",
                columns: new[] { "PersonAssessmentGradeId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamSubject",
                table: "ParamSubject",
                columns: new[] { "SubjectId", "SortOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSubject_Subjects_PSubjectId",
                table: "ParamSubject",
                column: "PSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrade_PersonGrades_PersonAssessmentGradeId",
                table: "SubjectGrade",
                column: "PersonAssessmentGradeId",
                principalTable: "PersonGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
