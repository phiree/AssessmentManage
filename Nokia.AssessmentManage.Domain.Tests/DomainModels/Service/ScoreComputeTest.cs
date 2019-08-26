using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
 
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using Nokia.AssessmentMange.Domain.DomainModels.Entity;

namespace Nokia.AssessmentManage.Domain.Tests.DomainModels.Service
{
    public class ScoreComputeTest
    {
        [Fact]
        public void GradeMatchExactly()
        {
            ScoreCompute compute=new ScoreCompute();
            var conversions=new List<ScoreConversion>{ new ScoreConversion("",new AgeRange(1,10), AssessmentMange.Domain.DomainModels.Sex.Female,10, 60) };
           var nearest= compute.GetNearest(conversions,10);
            Assert.Equal(60,nearest.Score);
        }
        [Fact]
        public void GradeOutOfRange()
        {
            ScoreCompute compute = new ScoreCompute();
            var conversions = new List<ScoreConversion> { new ScoreConversion("", new AgeRange(1, 10), AssessmentMange.Domain.DomainModels.Sex.Female, 10, 60) };
            var nearest = compute.GetNearest(conversions, 14);
            Assert.Equal(60, nearest.Score);
        }
    }
}
