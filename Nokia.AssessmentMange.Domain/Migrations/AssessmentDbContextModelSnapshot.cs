﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    [DbContext(typeof(AssessmentDbContext))]
    partial class AssessmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Assessment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Annual");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Entity.AssessmentSubject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssessmentId");

                    b.Property<string>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("AssessmentSubjects");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Person", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Birthday");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("RealName");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.PersonGrade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssessmentId");

                    b.Property<bool>("IsAbsent");

                    b.Property<bool>("IsMakeup");

                    b.Property<string>("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonGrades");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Subject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsQualifiedConversion");

                    b.Property<string>("Name");

                    b.Property<int>("SexLimitation");

                    b.Property<int>("SubjectType");

                    b.Property<string>("Unit");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.SubjectGrade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Grade");

                    b.Property<string>("PersonGradeId");

                    b.Property<string>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("PersonGradeId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectGrade");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LoginName");

                    b.Property<string>("Password");

                    b.Property<string>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Department", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Department", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Entity.AssessmentSubject", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Assessment", "Assessment")
                        .WithMany()
                        .HasForeignKey("AssessmentId");

                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Person", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.SubjectGrade", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.PersonGrade")
                        .WithMany("Grades")
                        .HasForeignKey("PersonGradeId");

                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.User", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });
#pragma warning restore 612, 618
        }
    }
}
