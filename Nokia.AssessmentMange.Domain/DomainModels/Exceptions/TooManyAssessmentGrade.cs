using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class TooManyAssessmentGrade:Exception
    {
        string assessmentName,personName;
        public TooManyAssessmentGrade(string assessmentName,string personName)
        { 
            this.assessmentName=assessmentName;
            this.personName=personName;
            }
        public override string Message =>$"人员[{personName}]已经录入了两次考核[{assessmentName}]成绩,不能再录入";
    }
}
