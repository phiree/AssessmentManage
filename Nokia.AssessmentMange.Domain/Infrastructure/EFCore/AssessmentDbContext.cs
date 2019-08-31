using Microsoft.EntityFrameworkCore;
using Nokia.AssessmentMange.Domain.DomainModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class AssessmentDbContext : DbContext
    {

        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
            : base(options) { }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AssessmentSubject> AssessmentSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<PersonAssessmentGrade> PersonGrades { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssessmentSubject>().HasKey(t => new { t.AssessmentId, t.SubjectId });
            modelBuilder.Entity<Person>().Ignore(x => x.Age);
            //科目-->成绩换算表
            modelBuilder.Entity<Subject>().OwnsMany(x => x.SubjectConversions, a =>
               {

                   a.HasForeignKey("SubjectId");
                   a.Property<Sex>("Sex");
                   a.HasKey("SubjectId", "Sex").HasName("SubjectConversionId");
                   a.OwnsOne(x => x.ConversionTable, b =>
                   {

                       b.OwnsMany(x => x.Grades, c =>
                       {
                           c.HasForeignKey("SubjectId", "Sex");
                           c.OwnsOne(x => x.AgeRange);
                           c.OwnsOne(x => x.Grade);

                       });
                   });

               });
            //modelBuilder.Entity<PersonAssessmentGrade>().OwnsMany(x=>x.SubjectGrades)
        }


    }
}
