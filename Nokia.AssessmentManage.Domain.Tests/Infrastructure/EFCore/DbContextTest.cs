using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nokia.AssessmentMange.Domain;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Infrastructure;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using MySql.Data.EntityFrameworkCore.Extensions;
using Autofac;
 
using System.Text;
 
using Newtonsoft.Json;
using Xunit;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Nokia.AssessmentManage.Domain.Tests.Infrastructure.EFCore
{
   public class DbContextTest
    {
        [Fact]
        public void Modify_SharedOwned()
        {
            var options = new DbContextOptionsBuilder<AssessmentDbContext>()
               .UseInMemoryDatabase(databaseName: "inmemo")
               .Options;
            var db = new AssessmentMange.Domain.Infrastructure.EFCore.AssessmentDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var subject=new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothAndSameConversion,false,"秒");
            
            db.Add(subject);
            db.SaveChanges();
            subject=db.Find<Subject>(subject.Id);
            var table = new ConversionTable().Init(new List<AgeRange> { new AgeRange(12,24),new AgeRange(25,27) }, new List<int> { 100,90 });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    Sex.Female, table
                    ));
            db.SaveChanges();
            
            //Assert.Equal(0,subject.SubjectConversions[0].ConversionTable.Grades[0].Grade.GradeValue);
        }
        [Fact]
        public void 值对象作为主键()
        {
            var options = new DbContextOptionsBuilder<AssessmentDbContextTest>()
               .UseInMemoryDatabase(databaseName: "inmemo")
               .Options;
            var db = new AssessmentDbContextTest(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var subject = new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothAndSameConversion, false, "秒");

            db.Add(subject);
            db.SaveChanges();
            subject = db.Find<Subject>(subject.Id);
            var table = new ConversionTable().Init(new List<AgeRange> { new AgeRange(12, 24) }, new List<int> { 100 });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    Sex.Female, table
                    ));

            db.SaveChanges();

            //  subject=db.Subjects.Find(subject.Id);
            //  subject.GetSubjectConversion(Sex.Female).ConversionTable.AddAgeRange(new AgeRange(26,30));
            //  db.SaveChanges();
            subject = db.Subjects.Find(subject.Id);
          
            var cell=subject.GetSubjectConversion(Sex.Female).ConversionTable.Grades[0];
            foreach(var entry in db.ChangeTracker.Entries())
            {
              string changed=string.Format("Entity: {0},  State: {1} ", entry.Entity.GetType().Name, entry.State.ToString());
Console.WriteLine(changed);
            }

            foreach(var a in subject.GetSubjectConversion(Sex.Female).ConversionTable.AgeRanges)
            {
              //  var ageRange=a.AgeRange;// new AgeRange(a.AgeRange.FloorAge,a.AgeRange.CellingAge);
                subject.GetSubjectConversion(Sex.Female).ConversionTable.Grades.Add(new ConversionCell(a, 90, Grade.NullGrade));
            }
           // worked subject.GetSubjectConversion(Sex.Female).ConversionTable.Grades.Add(new ConversionCell(new AgeRange(12, 24),90,Grade.NullGrade));
            db.SaveChanges();

            /*
     //
           subject.GetSubjectConversion(Sex.Female).ConversionTable.AddScore(90);
            db.Attach(cell);
          
            
            subject = db.Subjects.Find(subject.Id);
            subject.GetSubjectConversion(Sex.Female).ConversionTable.SetGrade(new AgeRange(12,24),100,13);
          //  db.SaveChanges();

            subject = db.Subjects.Find(subject.Id);
            subject.GetSubjectConversion(Sex.Female).ConversionTable.AddScore(90);
          
           // db.SaveChanges();

            //Assert.Equal(0,subject.SubjectConversions[0].ConversionTable.Grades[0].Grade.GradeValue);
            */
        }
        public class AssessmentDbContextTest : DbContext
        {

            public AssessmentDbContextTest(DbContextOptions<AssessmentDbContextTest> options)
                : base(options)
            {

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


                modelBuilder.Entity<PersonAssessmentGrade>().Property(x => x.Id).HasMaxLength(100);
                modelBuilder.Entity<Department>().HasIndex(p => new { p.Name, p.ParentId }).IsUnique(true);

                modelBuilder.Entity<PersonAssessmentGrade>()
                .OwnsMany(x => x.AssessmentGrades
                , m => {
                    m.HasForeignKey("PersonAssessmentGradeId");
                    m.Property(x => x.IsMakeup);//最多只有一次补考,可以作为联合组建
                    m.HasKey("PersonAssessmentGradeId", "IsMakeup").HasName("AssessmentGradeId");
                    m.OwnsMany(x => x.SubjectGrades, n => {
                        n.HasForeignKey("PersonAssessmentGradeId", "IsMakeup");
                        n.Property(x => x.SubjectId);
                        n.HasKey("PersonAssessmentGradeId", "IsMakeup", "SubjectId").HasName("PMS");
                    });
                });
                modelBuilder.Entity<AssessmentSubject>()
                    .Property(x => x.AssessmentId).HasMaxLength(100)
                    ;
                modelBuilder.Entity<AssessmentSubject>()
                  .Property(x => x.SubjectId).HasMaxLength(100)
                  ;
                modelBuilder.Entity<AssessmentSubject>()
                    .HasKey(k => new { k.SubjectId, k.AssessmentId })
                    .HasName("AssessmentSubjectId");
                modelBuilder.Entity<AssessmentSubject>()
                    .HasOne(a => a.Assessment)
                    .WithMany(a => a.Subjects)
                    .HasForeignKey(a => a.AssessmentId).HasConstraintName("AssessmentSubject_AssessmentId");
                modelBuilder.Entity<AssessmentSubject>()
                   .HasOne(a => a.Subject)
                   .WithMany(a => a.Assessments)
                   .HasForeignKey(a => a.SubjectId).HasConstraintName("AssessmentSubject_SubjectId");


                modelBuilder.Entity<Person>().Ignore(x => x.Age);
                //科目-->成绩换算表
                modelBuilder.Entity<Subject>().Property(x => x.Id).HasMaxLength(100);
                modelBuilder.Entity<Subject>().HasIndex(p => new { p.Name }).IsUnique(true);
                modelBuilder.Entity<Subject>()

                    .OwnsMany(x => x.SubjectConversions, a =>
                    {

                        a.HasForeignKey("SubjectId");
                        a.Property<Sex>(x => x.Sex).HasConversion(v => ((int)v).ToString(),
                v => (Sex)Enum.Parse(typeof(Sex), v))
                .IsUnicode(false);
                        a.HasKey("SubjectId", "Sex").HasName("SubjectConversionId");
                        a.OwnsOne( x => x.ConversionTable
                        , b =>
                        {
                            b.Ignore(x=>x.Scores);
                            b.Ignore(x=>x.AgeRanges);
                             // b.HasMany(x=>x.Grades );

                            b.OwnsMany(x => x.Grades, c =>
                            {
                                c.HasForeignKey("SubjectId", "Sex");
                               c.OwnsOne(x => x.AgeRange);
                                
                               //  c.Property<int>("FloorAge");//约定.会自动寻找子对象的同名属性?
                               // c.Property(x => x.FloorAgeAsKey).HasColumnName("FloorAgeAsKey");
                                c.OwnsOne(x => x.Grade);
                                //c.Property(x => x.Score);
                                c.HasKey("SubjectId", "Sex",   "Score", "FloorAgeAsKey");//.HasName("ConversionCellId");

                                //  c.HasIndex("SubjectId", "Sex", "FloorAgeAsKey", "Score").IsUnique(true).HasName("ConversionCellId");
                                // c.HasKey(x => x.Id);
                            });
                        }
                        );

                    });
                //modelBuilder.Entity<PersonAssessmentGrade>().OwnsMany(x=>x.SubjectGrades)

                //modelBuilder.Entity<ConversionCell>()
                //    .OwnsOne(x=>x.AgeRange);

                modelBuilder.Entity<ComputedSubject>().OwnsMany(x => x.ParamSubjects, m => {
                    m.HasForeignKey("SubjectId");
                    m.Property(x => x.SortOrder);
                    m.Property(x => x.PSubjectId);
                    m.HasKey("SubjectId", "SortOrder", "PSubjectId");
                });
            }

        }
    }
}
