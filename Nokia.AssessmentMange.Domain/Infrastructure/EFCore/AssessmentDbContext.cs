﻿using Microsoft.EntityFrameworkCore;
using Nokia.AssessmentMange.Domain.DomainModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class AssessmentDbContext : DbContext
    {

        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
            : base(options) {
          
            }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ComputedSubject> ComputedSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<PersonAssessmentGrade> PersonGrades { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<PersonAssessmentGrade>().Property(x=>x.Id).HasMaxLength(100);
            modelBuilder.Entity<Department>().HasIndex(p=>new { p.Name,p.ParentId}).IsUnique(true);

            modelBuilder.Entity<PersonAssessmentGrade>()
                .OwnsMany(x=>x.SubjectGrades
                ,m=>{ 
                    m.HasForeignKey("PersonAssessmentGradeId");
                    m.Property(x=>x.SubjectId);
                    m.HasKey("PersonAssessmentGradeId","SubjectId").HasName("SubjectGradeId");
                    });
            modelBuilder.Entity<AssessmentSubject>()
                .Property(x=>x.AssessmentId).HasMaxLength(100)
                ;
            modelBuilder.Entity<AssessmentSubject>()
              .Property(x => x.SubjectId).HasMaxLength(100)
              ;
            modelBuilder.Entity<AssessmentSubject>()
                .HasKey(k=>new{ k.SubjectId,k.AssessmentId })
                .HasName("AssessmentSubjectId");
            modelBuilder.Entity<AssessmentSubject>()
                .HasOne(a=>a.Assessment)
                .WithMany(a=>a.Subjects)
                .HasForeignKey(a=>a.AssessmentId).HasConstraintName("AssessmentSubject_AssessmentId");
            modelBuilder.Entity<AssessmentSubject>()
               .HasOne(a => a.Subject)
               .WithMany(a => a.Assessments)
               .HasForeignKey(a => a.SubjectId).HasConstraintName("AssessmentSubject_SubjectId");


            modelBuilder.Entity<Person>().Ignore(x => x.Age);
            //科目-->成绩换算表
            modelBuilder.Entity<Subject>().Property(x=>x.Id).HasMaxLength(100);
            modelBuilder.Entity<Subject>(). HasIndex(p=>new {p.Name }).IsUnique(true);
            modelBuilder.Entity<Subject>()
               
                .OwnsMany(x => x.SubjectConversions, a =>
               {

                   a.HasForeignKey("SubjectId");
                   a.Property<Sex?>("Sex");
                   a.HasKey("SubjectId", "Sex").HasName("SubjectConversionId");
                   a.OwnsOne(x => x.ConversionTable, b =>
                   {

                       b.OwnsMany(x => x.Grades, c =>
                       {
                           c.HasForeignKey("SubjectId", "Sex");
                           c.OwnsOne(x => x.AgeRange);
                           c.Property<int>("FloorAge");//约定.会自动寻找子对象的同名属性?
                           c.OwnsOne(x => x.Grade);
                           c.HasKey("SubjectId","Sex", "FloorAge", "Score").HasName("ConversionCellId");

                       });
                   });

               });
            //modelBuilder.Entity<PersonAssessmentGrade>().OwnsMany(x=>x.SubjectGrades)

            modelBuilder.Entity<ComputedSubject>().OwnsMany(x=>x.ParamSubjects,m=>{ 
                m.HasForeignKey("SubjectId");
                m.Property(x=>x.SortOrder);
                m.Property(x => x.PSubjectId);
                m.HasKey("SubjectId","SortOrder","PSubjectId");
                });
        }
       
    }
}
