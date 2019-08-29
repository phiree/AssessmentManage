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
            modelBuilder.Entity<AssessmentSubject>()
           .HasKey(t => new { t.AssessmentId, t.SubjectId});
            
        }


    }
}
