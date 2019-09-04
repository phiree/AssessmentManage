using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
 using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;
namespace Nokia.AssessmentManage.Domain.Tests.DomainModels.Entity
{
    public class ConversionTableTests
    {
        [Fact]
        public void AddScoreWithOutInitTest()
        {
          ConversionTable table=new ConversionTable();
          Assert.Throws<ConversionTableNotInitialized>(()=>{
              table.AddAgeRange(new AgeRange(10,20));
              });
            Assert.Equal(0,table.Grades.Count);
        }
       
        [Fact]
        public void AddCoincideScore ()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 13 });
            
            Assert.Throws<AgeRangeCoincide>(() => {
                table.AddAgeRange(new AgeRange(9, 10)); 
                });
            Assert.Throws<AgeRangeCoincide>(() => {
                table.AddAgeRange(new AgeRange(9, 11));
            });
            Assert.Throws<AgeRangeCoincide>(() => {
                table.AddAgeRange(new AgeRange(20, 21));
            });
            
        }
        [Fact]
        public void AddAlreadyExistedScore()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 13 });

            Assert.Throws<ScoreAlreadyExisted>(() => {
                table.AddScore(13);
            });
            

        }
        [Fact]
        public void AddScoreTest()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 13 });
            // table.AddAgeRange
            table.AddScore(14);
            Assert.Equal(2, table.Grades.Count);
            Assert.Equal(0,table.Grades[1].Grade.GradeValue);
            Assert.Throws<ScoreAlreadyExisted>(() => {
                table.AddScore(13);
            });
        }
        [Fact]
        public void AddAgeRangeTEst()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 13 });

            table.AddAgeRange(new AgeRange(21,23));
           
        }
        [Fact]
        public void 设置分数_没有对应的单元格()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 100 });
            Assert.Throws<ConversionCellNotFound>(() => { 
           table.SetGrade(new AgeRange(10,21),100,13);
            });
             
        }
        [Fact]
        public void 设置分数()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 100 });
            
                table.SetGrade(new AgeRange(10, 20), 100, 13);
           Assert.Equal(13,table.Grades[0].Grade.GradeValue);

        }
        [Fact]
        public void 增加年龄段和分数()
        {
            ConversionTable table = new ConversionTable();
            table.Init(new List<AgeRange> { new AgeRange(10, 20) }, new List<int> { 100 });

            table.AddAgeRange(new AgeRange(21, 30));
            table.AddAgeRange(new AgeRange(31, 40));
            table.AddScore(80);
            table.AddScore(90);
            table.AddScore(70);
            Assert.Equal(12,table.Grades.Count);

        }

    }
}
