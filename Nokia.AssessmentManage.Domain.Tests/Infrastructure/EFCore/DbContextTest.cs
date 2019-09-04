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
        public void 成绩对照单()
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
            var table = new ConversionTable().Init(new List<AgeRange> { new AgeRange(12,24) }, new List<int> { 100 });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    Sex.Female, table
                    ));
            ConversionTable conversion;
            db.SaveChanges();

            AgeRange ageRange=null;
            //增加年龄
            conversion = db.Subjects.Find(subject.Id).GetSubjectConversion(Sex.Female).ConversionTable;
           conversion.AddAgeRange(new AgeRange(29,30));
            db.SaveChanges();
            //增加分数
            conversion = db.Subjects.Find(subject.Id).GetSubjectConversion(Sex.Female).ConversionTable;
            conversion.AddScore(80);
            db.SaveChanges();
            //增加年龄
              ageRange= new AgeRange(31, 38);
            conversion = db.Subjects.Find(subject.Id).GetSubjectConversion(Sex.Female).ConversionTable;
            conversion.AddAgeRange(ageRange);
            ConversionCell cell=new ConversionCell(new AgeRange(31, 38),90,new Grade(12));
          
            conversion.Grades.Add(cell);
            db.SaveChanges();
            //增加分数
            
            conversion = db.Subjects.Find(subject.Id).GetSubjectConversion(Sex.Female).ConversionTable;
            conversion.AddScore(180);
            
            db.SaveChanges();
            //增加年龄
            conversion = db.Subjects.Find(subject.Id).GetSubjectConversion(Sex.Female).ConversionTable;
            conversion.AddAgeRange(new AgeRange(39, 43));
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

            foreach(var a in subject.GetSubjectConversion(Sex.Female).ConversionTable.AgeRangeList2)
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

        [Fact]
        public void 录入人员成绩()
        {
            var options = new DbContextOptionsBuilder<AssessmentDbContextTest>()
               .UseInMemoryDatabase(databaseName: "inmemo")
               .Options;
            var db = new AssessmentDbContextTest(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //添加一个科目
            var subject = new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothAndSameConversion, false, "秒");
            db.Add(subject);
            db.SaveChanges();
            //添加一个得分换算表
            subject = db.Find<Subject>(subject.Id);
            var table = new ConversionTable().Init(new List<AgeRange> { new AgeRange(12, 24) }, new List<int> { 100 });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    Sex.Female, table
                    ));
            
            subject.GetSubjectConversion(Sex.Female).ConversionTable.SetGrade(new AgeRange(12, 24), 100, 14);
            db.SaveChanges();

            //添加一个年龄段
             subject=db.Subjects.Find(subject.Id);
             subject.GetSubjectConversion(Sex.Female).ConversionTable.AddAgeRange(new AgeRange(26,30));
            subject.GetSubjectConversion(Sex.Female).ConversionTable.SetGrade(new AgeRange(26,30),100,13 );
            db.SaveChanges();

            //增加一个部门
            Department department=new Department("一连",null);
            db.Add(department);
            db.SaveChanges();
            //增加一个人员
            Person person=new Person("zhangsan", new DateTime(1991,10,10),Sex.Female,department.Id);
            db.Add(person);
            db.SaveChanges();
            //增加一个考核
            Assessment assessment=new Assessment(department.Id,"9月考核",false);
            db.Add(assessment);
            db.SaveChanges();
            //考核增加一个科目
            assessment.Subjects=new List<AssessmentSubject> {new AssessmentSubject(assessment,subject) };
            db.SaveChanges();
            //添加人员考核.
            PersonAssessmentGrade personAssessmentGrade=new PersonAssessmentGrade(assessment,person);
            db.Add(personAssessmentGrade);
            db.SaveChanges();
            //录入成绩
            personAssessmentGrade=db.PersonGrades.Find(personAssessmentGrade.Id);
            personAssessmentGrade.CommitGrade(new AssessmentGrade(false,false,new List<SubjectGrade>{new SubjectGrade(subject.Id,13)}));
            db.SaveChanges();

            personAssessmentGrade= db.PersonGrades.Find(personAssessmentGrade.Id);

            Assert.Equal(13,personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);
            //第二次录入_非补考
            personAssessmentGrade.CommitGrade(new AssessmentGrade(false, false, new List<SubjectGrade> { new SubjectGrade(subject.Id, 14) }));

            personAssessmentGrade = db.PersonGrades.Find(personAssessmentGrade.Id);
            Assert.Equal(1, personAssessmentGrade.AssessmentGrades.Count);
            Assert.Equal(14, personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);
            //第二次录入_补考
            personAssessmentGrade.CommitGrade(new AssessmentGrade(false, true, new List<SubjectGrade> { new SubjectGrade(subject.Id, 19) }));

            personAssessmentGrade = db.PersonGrades.Find(personAssessmentGrade.Id);
            Assert.Equal(2, personAssessmentGrade.AssessmentGrades.Count);
            Assert.Equal(19, personAssessmentGrade.AssessmentGrades.First(x=>x.IsMakeup).SubjectGrades.First().Grade);
            Assert.Equal(14, personAssessmentGrade.AssessmentGrades.First(x => !x.IsMakeup).SubjectGrades.First().Grade);

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
                            b.Ignore(x=>x.AgeRangeList2);
                             // b.HasMany(x=>x.Grades );

                            b.OwnsMany(x => x.Grades, c =>
                            {
                                c.Ignore(x=>x.AgeRange);
                                c.HasForeignKey("SubjectId", "Sex");
                                c.Property(x => x.FloorAgeAsKey);
                                c.Property(x=>x.Score);
                               c.OwnsOne(x => x.AgeRange);
                                
                               //  c.Property<int>("FloorAge");//约定.会自动寻找子对象的同名属性?
                           
                                c.OwnsOne(x => x.Grade);
                                //c.Property(x => x.Score);
                                c.HasKey("SubjectId", "Sex", "FloorAgeAsKey",  "Score");//.HasName("ConversionCellId");

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
