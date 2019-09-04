using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class SubjectNotInAssessment:Exception
    {
        string assessmentName,  subjectName;
        public SubjectNotInAssessment(string assessmentName,string subjectName)
        {this.assessmentName=assessmentName;this.subjectName=subjectName ;}
        public override string Message => $"提交成绩错误.考核[{assessmentName}]不包含科目[{subjectName}]";
    }
}
