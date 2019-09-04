using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class DuplicateSubjectGrade:Exception
    {
        string personName,assessmentName,subjectName;
        public DuplicateSubjectGrade(string personName, string assessmentName, string subjectName)
        { 
            this.personName=personName;
            this.assessmentName=assessmentName;
            this.subjectName=subjectName;
            }

        public override string Message =>$"错误.人员[{personName}]的考核[{assessmentName}]成绩中有重复的科目[{subjectName}]";
    }
}
