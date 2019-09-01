using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
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
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
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
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    SubjectType = table.Column<int>(nullable: false),
                    SexLimitation = table.Column<int>(nullable: false),
                    IsQualifiedConversion = table.Column<short>(type: "bit", nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Formula = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    RealName = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
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
                name: "AssessmentSubject",
                columns: table => new
                {
                    AssessmentId = table.Column<string>(maxLength: 100, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssessmentSubjectId", x => new { x.SubjectId, x.AssessmentId });
                    table.ForeignKey(
                        name: "AssessmentSubject_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "AssessmentSubject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamSubject",
                columns: table => new
                {
                    SortOrder = table.Column<int>(nullable: false),
                    SubjectId = table.Column<string>(nullable: false),
                    PSubjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamSubject", x => new { x.SubjectId, x.SortOrder });
                    table.ForeignKey(
                        name: "FK_ParamSubject_Subjects_PSubjectId",
                        column: x => x.PSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParamSubject_Subjects_SubjectId",
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
                    table.PrimaryKey("SubjectConversionId", x => new { x.SubjectId, x.Sex });
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
                    Id = table.Column<string>(maxLength: 100, nullable: false),
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
                    Id = table.Column<string>(maxLength: 100, nullable: false),
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
                name: "ConversionCell",
                columns: table => new
                {
                    Score = table.Column<double>(nullable: false),
                    SubjectId = table.Column<string>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    FloorAge = table.Column<int>(nullable: false),
                    AgeRange_Minimum = table.Column<int>(nullable: false),
                    AgeRange_Maximum = table.Column<int>(nullable: false),
                    AgeRange_CellingAge = table.Column<int>(nullable: false),
                    AgeRange_FloorAge = table.Column<int>(nullable: false),
                    Grade_GradeValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ConversionCellId", x => new { x.SubjectId, x.Sex, x.FloorAge, x.Score });
                    table.ForeignKey(
                        name: "FK_ConversionCell_SubjectConversion_SubjectId_Sex",
                        columns: x => new { x.SubjectId, x.Sex },
                        principalTable: "SubjectConversion",
                        principalColumns: new[] { "SubjectId", "Sex" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrade",
                columns: table => new
                {
                    SubjectId = table.Column<string>(nullable: false),
                    PersonAssessmentGradeId = table.Column<string>(nullable: false),
                    Grade = table.Column<double>(nullable: true),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SubjectGradeId", x => new { x.PersonAssessmentGradeId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SubjectGrade_PersonGrades_PersonAssessmentGradeId",
                        column: x => x.PersonAssessmentGradeId,
                        principalTable: "PersonGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGrade_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSubject_AssessmentId",
                table: "AssessmentSubject",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name_ParentId",
                table: "Departments",
                columns: new[] { "Name", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamSubject_PSubjectId",
                table: "ParamSubject",
                column: "PSubjectId");

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
                name: "IX_SubjectGrade_SubjectId",
                table: "SubjectGrade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSubject");

            migrationBuilder.DropTable(
                name: "ConversionCell");

            migrationBuilder.DropTable(
                name: "ParamSubject");

            migrationBuilder.DropTable(
                name: "SubjectGrade");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubjectConversion");

            migrationBuilder.DropTable(
                name: "PersonGrades");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
