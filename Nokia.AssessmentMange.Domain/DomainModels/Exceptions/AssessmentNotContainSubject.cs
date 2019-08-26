using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public  class AssessmentNotContainSubject:Exception
    {
        string assessmentName,subjectName;
        public AssessmentNotContainSubject(string assessmentName,string subjectName)
        { 
            this.assessmentName=assessmentName;
            this.subjectName=subjectName;
            }
        public override string Message =>$"成绩录入错误.该考核[{assessmentName}]不包含科目[{subjectName}],无法录入";
    }
}
