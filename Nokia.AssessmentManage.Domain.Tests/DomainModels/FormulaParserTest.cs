using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nokia.AssessmentManage.Domain.Tests.DomainModels
{
   public class FormulaParserTest
    {
        [Fact]
        public void ParseTest()
        {
            var parser=new FormulaParser();
           string code= parser.Parse("$1/$2",new Dictionary<int,double?>{ {1,13 },{ 2,14} });

            Assert.Equal("13m/14m",code);
            }
    }
}
