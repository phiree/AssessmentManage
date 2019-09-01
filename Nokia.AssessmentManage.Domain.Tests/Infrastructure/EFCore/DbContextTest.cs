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
            var table = new ConversionTable().Init(new List<AgeRange> { new AgeRange(12,24) }, new List<double> { 100 });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    Sex.Female, table
                    ));
            db.SaveChanges();
            
            //Assert.Equal(0,subject.SubjectConversions[0].ConversionTable.Grades[0].Grade.GradeValue);
        }
    }
}
