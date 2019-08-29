using Microsoft.EntityFrameworkCore;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
   public  class AssessmentDbContext:DbContext
    {
        
        public AssessmentDbContext( DbContextOptions<AssessmentDbContext> options)
            :base(options) { }
        public DbSet<Assessment> Assessments { get;set;}
        public DbSet<Subject>  Subjects{ get; set; }
        public DbSet<AssessmentSubject> AssessmentSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<PersonAssessmentGrade>  PersonGrades{ get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssessmentSubject>() .HasKey(t => new { t.AssessmentId, t.SubjectId});
            //modelBuilder.Entity<AgeConversion>().OwnsOne(x=>x.AgeRange);
            //modelBuilder.Entity<AgeConversion>().OwnsMany(x=>x.ScoreGrades,a=>{ 
            //    a.HasForeignKey("AgeConversionId");
            //    a.Property<double>("Score");
            //    a.HasKey("AgeConversionId","Score");
            //    });
            modelBuilder.Entity<Subject>().OwnsMany(x=>x.SubjectConversions,a=>
            { 
                
                a.HasForeignKey("SubjectId");
                a.Property<Sex>("Sex");
                a.HasKey("SubjectId","Sex");//("SubjectConversionId");
                a.OwnsMany(x => x.AgeConversions, b => {
                    b.HasForeignKey("SubjectId","Sex");
                    b.OwnsOne(x => x.AgeRange);
                    b.Property<int>("FloorAge");
                    b.HasKey("SubjectId", "Sex", "FloorAge");//.HasName("AgeConversionId");
                    b.OwnsMany(x=>x.ScoreGrades,c=>
                    {
                        c.HasForeignKey("SubjectId", "Sex", "FloorAge");
                        c.Property<double>("Score");
                        c.HasKey("SubjectId", "Sex", "FloorAge", "Score");//.HasName("ScoreGradeId");
                    });
                });

                });

        }


    }
}
