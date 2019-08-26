using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 得分换算
    /// </summary>
    public class AgeConversion : Entity.EntityBase
    {
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
    /// <summary>
    /// 年龄范围
    /// </summary>
    public class AgeRange
    {
        
        public AgeRange(  int floorAge,int cellingAge)
        { 
            this.CellingAge=cellingAge;
            this.FloorAge=floorAge;
            }
        public int CellingAge { get; protected set; }
        public int FloorAge { get; protected set; }
        public bool InRange(int age)
        { 
             return age>=FloorAge&&age<=CellingAge;
            }
    }

    public class  ScoreGrade
    { 
        public ScoreGrade(double grade,double score)
        { 
            this.Grade=grade;
            this.Score=score;
            }
        public double Grade { get;protected set;}
        public double Score { get;protected set;}
        }

    
}
