using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class FirstAssessmentGradeShouldNotBeMakeup:Exception
    {
        string assessmentName, personName;
        public FirstAssessmentGradeShouldNotBeMakeup(string assessmentName, string personName)
        {
            this.assessmentName = assessmentName;
            this.personName = personName;
        }
        
        public override string Message => "错误.人员[{personName}]考核[{assessmentName}]的第一次成绩不应该是补考成绩";
    }
}
