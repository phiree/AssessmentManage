using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class ConversionCellNotFound:Exception
    {
        AgeRange ageRange;double score;
        public ConversionCellNotFound(AgeRange ageRange,double score)
        { 
            this.ageRange=ageRange;
            this.score=score;
            }
        public override string Message =>$"对照表中没有对应的坐标:年龄段[{ageRange.FloorAge},{ageRange.CellingAge}] ; 分数[{score}]";
    }
}
