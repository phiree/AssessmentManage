using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 插值算法
    /// </summary>
    public class InterpolationScore
    {
        double scoreFirst,scoreSecond,gradeFirst,gradeSecond,grade;
        public InterpolationScore(double scoreFirst, double scoreSecond, double gradeFirst, double gradeSecond, double  grade)
        { this.scoreFirst = scoreFirst;
            this.scoreSecond = scoreSecond;
            this.gradeFirst = gradeFirst;
            this.gradeSecond = gradeSecond;
            this.grade=grade;
           
            
            }
        public   double GetValue( )
        {
            var vaule = scoreSecond -  (gradeSecond- grade) *(scoreSecond-scoreFirst)/(gradeSecond-gradeFirst);

           return vaule;
        }
    }
}
