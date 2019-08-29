using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Annual = table.Column<short>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SubjectType = table.Column<int>(nullable: false),
                    SexLimitation = table.Column<int>(nullable: false),
                    IsQualifiedConversion = table.Column<short>(type: "bit", nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RealName = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentSubjects",
                columns: table => new
                {
                    AssessmentId = table.Column<string>(nullable: false),
                    SubjectId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSubjects", x => new { x.AssessmentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AssessmentSubjects_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectConversion",
                columns: table => new
                {
                    Sex = table.Column<int>(nullable: false),
                    SubjectId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectConversion", x => new { x.SubjectId, x.Sex });
                    table.ForeignKey(
                        name: "FK_SubjectConversion_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonGrades",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssessmentId = table.Column<string>(nullable: true),
                    PersonId = table.Column<string>(nullable: true),
                    IsAbsent = table.Column<short>(nullable: false),
                    IsMakeup = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonGrades_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonGrades_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LoginName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PersonId = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgeConversion",
                columns: table => new
                {
                    SubjectId = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    FloorAge = table.Column<int>(nullable: false),
                    AgeRange_CellingAge = table.Column<int>(nullable: false),
                    AgeRange_FloorAge = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeConversion", x => new { x.SubjectId, x.Sex, x.FloorAge });
                    table.ForeignKey(
                        name: "FK_AgeConversion_SubjectConversion_SubjectId_Sex",
                        columns: x => new { x.SubjectId, x.Sex },
                        principalTable: "SubjectConversion",
                        principalColumns: new[] { "SubjectId", "Sex" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrade",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SubjectId = table.Column<string>(nullable: true),
                    Grade = table.Column<double>(nullable: true),
                    PersonAssessmentGradeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGrade_PersonGrades_PersonAssessmentGradeId",
                        column: x => x.PersonAssessmentGradeId,
                        principalTable: "PersonGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreGrade",
                columns: table => new
                {
                    Score = table.Column<double>(nullable: false),
                    SubjectId = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    FloorAge = table.Column<int>(nullable: false),
                    Grade = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreGrade", x => new { x.SubjectId, x.Sex, x.FloorAge, x.Score });
                    table.ForeignKey(
                        name: "FK_ScoreGrade_AgeConversion_SubjectId_Sex_FloorAge",
                        columns: x => new { x.SubjectId, x.Sex, x.FloorAge },
                        principalTable: "AgeConversion",
                        principalColumns: new[] { "SubjectId", "Sex", "FloorAge" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSubjects_SubjectId",
                table: "AssessmentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_DepartmentId",
                table: "Person",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGrades_AssessmentId",
                table: "PersonGrades",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGrades_PersonId",
                table: "PersonGrades",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrade_PersonAssessmentGradeId",
                table: "SubjectGrade",
                column: "PersonAssessmentGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrade_SubjectId",
                table: "SubjectGrade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSubjects");

            migrationBuilder.DropTable(
                name: "ScoreGrade");

            migrationBuilder.DropTable(
                name: "SubjectGrade");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AgeConversion");

            migrationBuilder.DropTable(
                name: "PersonGrades");

            migrationBuilder.DropTable(
                name: "SubjectConversion");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
