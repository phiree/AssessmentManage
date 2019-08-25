using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Entity
{
    /// <summary>
    /// 成绩换算表
    /// </summary>
    public class ScoreConversion : Entity.EntityBase
    {
        public AgeRange AgeRange { get; set; }
        public Sex Sex { get; set; }
        public virtual string GetScore()
        { throw new NotImplementedException(); }

    }

    /// <summary>
    /// 年龄范围
    /// </summary>
    public class AgeRange
    {
        public int BeginAge { get; protected set; }
        public int EndAge { get; protected set; }
    }

    public class QualifiedScore:ScoreConversion
    {
        public override string GetScore()
        {
            return  IsQualified?"合格":"不合格";
        }
        public bool IsQualified { get; protected set; }
    }
    public class DigitalScore:ScoreConversion
    {
        public override string GetScore()
        {
            return Score.ToString("{0:2}");
        }
        public double Score { get; protected set; }
    }
}
