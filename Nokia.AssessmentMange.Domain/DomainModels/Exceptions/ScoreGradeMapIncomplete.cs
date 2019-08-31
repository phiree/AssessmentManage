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
        
        double grade;
        public ScoreGradeMapIncomplete( double grade)
        { 
          
            this.grade = grade;
            }
        public override string Message => $" 成绩换算数据不全,无法计算成绩[{grade}]的得分";
    }
}
