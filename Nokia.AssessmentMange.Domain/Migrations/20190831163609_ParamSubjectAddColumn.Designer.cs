﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;

namespace Nokia.AssessmentMange.Domain.Migrations
{
    [DbContext(typeof(AssessmentDbContext))]
    [Migration("20190831163609_ParamSubjectAddColumn")]
    partial class ParamSubjectAddColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Assessment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<short>("Annual")
                        .HasColumnName("Annual")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.AssessmentSubject", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasMaxLength(100);

                    b.Property<string>("AssessmentId")
                        .HasMaxLength(100);

                    b.HasKey("SubjectId", "AssessmentId")
                        .HasName("AssessmentSubjectId");

                    b.HasIndex("AssessmentId");

                    b.ToTable("AssessmentSubject");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("Name", "ParentId")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Person", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("RealName");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.PersonAssessmentGrade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("AssessmentId");

                    b.Property<bool>("IsAbsent");

                    b.Property<bool>("IsMakeup");

                    b.Property<string>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonGrades");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Subject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<short>("IsQualifiedConversion")
                        .HasColumnName("IsQualifiedConversion")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("SexLimitation");

                    b.Property<int>("SubjectType");

                    b.Property<string>("Unit");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Subject");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LoginName");

                    b.Property<string>("Password");

                    b.Property<string>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.ComputedSubject", b =>
                {
                    b.HasBaseType("Nokia.AssessmentMange.Domain.DomainModels.Subject");

                    b.Property<string>("Formula");

                    b.HasDiscriminator().HasValue("ComputedSubject");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.AssessmentSubject", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Assessment", "Assessment")
                        .WithMany("Subjects")
                        .HasForeignKey("AssessmentId")
                        .HasConstraintName("AssessmentSubject_AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject", "Subject")
                        .WithMany("Assessments")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("AssessmentSubject_SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Department", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Department", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Person", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.PersonAssessmentGrade", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Assessment", "Assessment")
                        .WithMany()
                        .HasForeignKey("AssessmentId");

                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.OwnsMany("Nokia.AssessmentMange.Domain.DomainModels.SubjectGrade", "SubjectGrades", b1 =>
                        {
                            b1.Property<string>("PersonAssessmentGradeId");

                            b1.Property<string>("SubjectId");

                            b1.Property<double?>("Grade");

                            b1.Property<double>("Score");

                            b1.HasKey("PersonAssessmentGradeId", "SubjectId")
                                .HasName("SubjectGradeId");

                            b1.HasIndex("SubjectId");

                            b1.ToTable("SubjectGrade");

                            b1.HasOne("Nokia.AssessmentMange.Domain.DomainModels.PersonAssessmentGrade")
                                .WithMany("SubjectGrades")
                                .HasForeignKey("PersonAssessmentGradeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject", "Subject")
                                .WithMany()
                                .HasForeignKey("SubjectId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.Subject", b =>
                {
                    b.OwnsMany("Nokia.AssessmentMange.Domain.DomainModels.SubjectConversion", "SubjectConversions", b1 =>
                        {
                            b1.Property<string>("SubjectId");

                            b1.Property<int>("Sex");

                            b1.HasKey("SubjectId", "Sex")
                                .HasName("SubjectConversionId");

                            b1.ToTable("SubjectConversion");

                            b1.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject")
                                .WithMany("SubjectConversions")
                                .HasForeignKey("SubjectId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Nokia.AssessmentMange.Domain.DomainModels.ConversionTable", "ConversionTable", b2 =>
                                {
                                    b2.Property<string>("SubjectConversionSubjectId");

                                    b2.Property<int>("SubjectConversionSex");

                                    b2.HasKey("SubjectConversionSubjectId", "SubjectConversionSex");

                                    b2.ToTable("SubjectConversion");

                                    b2.HasOne("Nokia.AssessmentMange.Domain.DomainModels.SubjectConversion")
                                        .WithOne("ConversionTable")
                                        .HasForeignKey("Nokia.AssessmentMange.Domain.DomainModels.ConversionTable", "SubjectConversionSubjectId", "SubjectConversionSex")
                                        .OnDelete(DeleteBehavior.Cascade);

                                    b2.OwnsMany("Nokia.AssessmentMange.Domain.DomainModels.ConversionCell", "Grades", b3 =>
                                        {
                                            b3.Property<string>("SubjectId");

                                            b3.Property<int>("Sex");

                                            b3.Property<int>("FloorAge");

                                            b3.Property<double>("Score");

                                            b3.HasKey("SubjectId", "Sex", "FloorAge", "Score")
                                                .HasName("ConversionCellId");

                                            b3.ToTable("ConversionCell");

                                            b3.HasOne("Nokia.AssessmentMange.Domain.DomainModels.ConversionTable")
                                                .WithMany("Grades")
                                                .HasForeignKey("SubjectId", "Sex")
                                                .OnDelete(DeleteBehavior.Cascade);

                                            b3.OwnsOne("Nokia.AssessmentMange.Domain.DomainModels.AgeRange", "AgeRange", b4 =>
                                                {
                                                    b4.Property<string>("ConversionCellSubjectId");

                                                    b4.Property<int>("ConversionCellSex");

                                                    b4.Property<int>("ConversionCellFloorAge");

                                                    b4.Property<double>("ConversionCellScore");

                                                    b4.Property<int>("CellingAge");

                                                    b4.Property<int>("FloorAge");

                                                    b4.Property<int>("Maximum");

                                                    b4.Property<int>("Minimum");

                                                    b4.HasKey("ConversionCellSubjectId", "ConversionCellSex", "ConversionCellFloorAge", "ConversionCellScore");

                                                    b4.ToTable("ConversionCell");

                                                    b4.HasOne("Nokia.AssessmentMange.Domain.DomainModels.ConversionCell")
                                                        .WithOne("AgeRange")
                                                        .HasForeignKey("Nokia.AssessmentMange.Domain.DomainModels.AgeRange", "ConversionCellSubjectId", "ConversionCellSex", "ConversionCellFloorAge", "ConversionCellScore")
                                                        .OnDelete(DeleteBehavior.Cascade);
                                                });

                                            b3.OwnsOne("Nokia.AssessmentMange.Domain.DomainModels.Grade", "Grade", b4 =>
                                                {
                                                    b4.Property<string>("ConversionCellSubjectId");

                                                    b4.Property<int>("ConversionCellSex");

                                                    b4.Property<int>("ConversionCellFloorAge");

                                                    b4.Property<double>("ConversionCellScore");

                                                    b4.Property<double>("GradeValue");

                                                    b4.HasKey("ConversionCellSubjectId", "ConversionCellSex", "ConversionCellFloorAge", "ConversionCellScore");

                                                    b4.ToTable("ConversionCell");

                                                    b4.HasOne("Nokia.AssessmentMange.Domain.DomainModels.ConversionCell")
                                                        .WithOne("Grade")
                                                        .HasForeignKey("Nokia.AssessmentMange.Domain.DomainModels.Grade", "ConversionCellSubjectId", "ConversionCellSex", "ConversionCellFloorAge", "ConversionCellScore")
                                                        .OnDelete(DeleteBehavior.Cascade);
                                                });
                                        });
                                });
                        });
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.User", b =>
                {
                    b.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("Nokia.AssessmentMange.Domain.DomainModels.ComputedSubject", b =>
                {
                    b.OwnsMany("Nokia.AssessmentMange.Domain.DomainModels.ParamSubject", "ParamSubjects", b1 =>
                        {
                            b1.Property<string>("SubjectId");

                            b1.Property<int>("SortOrder");

                            b1.Property<string>("PSubjectId");

                            b1.Property<string>("PSubjectName");

                            b1.HasKey("SubjectId", "SortOrder");

                            b1.HasIndex("PSubjectId");

                            b1.ToTable("ParamSubject");

                            b1.HasOne("Nokia.AssessmentMange.Domain.DomainModels.Subject", "PSubject")
                                .WithMany()
                                .HasForeignKey("PSubjectId");

                            b1.HasOne("Nokia.AssessmentMange.Domain.DomainModels.ComputedSubject")
                                .WithMany("ParamSubjects")
                                .HasForeignKey("SubjectId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
