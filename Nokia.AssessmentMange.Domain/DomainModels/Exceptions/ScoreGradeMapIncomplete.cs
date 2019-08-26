using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    /// <summary>
    /// 成绩对照表不全
    /// </summary>
    public class ScoreGradeMapIncomplete:Exception
    {
        string subjectName;
        double grade;
        public ScoreGradeMapIncomplete(string subjectName,double grade)
        { 
            this.subjectName=subjectName;
            this.grade = grade;
            }
        public override string Message => $"科目[{subjectName}]对照项不全,无法计算成绩[{grade}]的得分";
    }
}
