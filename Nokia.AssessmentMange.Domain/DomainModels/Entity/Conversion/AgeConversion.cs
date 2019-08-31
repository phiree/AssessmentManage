using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 得分换算--年龄段的值.
    /// </summary>
    public class AgeConversion 
    {
        protected AgeConversion() { }
        public AgeConversion( AgeRange ageRange, IList<ScoreGrade> scoreGrades)
        { 
            this.AgeRange=ageRange;
            this.ScoreGrades= scoreGrades;
            }

      
        public AgeRange AgeRange { get; set; }
        /// <summary>
        /// 成绩对应的得分. 如果是 合格/不合格, 则为 1:合格,0:不合格
        /// </summary>
        public IList<ScoreGrade> ScoreGrades { get;protected set;}
        
      
    }
     

    
}
