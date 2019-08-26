using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
 
namespace Nokia.AssessmentManage.Domain.Tests.DomainModels
{
  
    public class InterpolationScoreTEsts
    {
        [Fact]
        public void GetValueTest()
        {

            var value=new InterpolationScore(85,80,22,24,23).GetValue();
            Assert.Equal(82.5,value);



            }
    }
}
