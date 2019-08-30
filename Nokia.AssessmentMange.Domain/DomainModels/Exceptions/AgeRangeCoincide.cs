using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class AgeRangeCoincide:Exception
    {
        AgeRange ageRange;
        public AgeRangeCoincide(AgeRange newAgeRange)
        { this.ageRange= newAgeRange;
            
            }
        public override string Message =>$"添加的时间段[{ageRange.FloorAge},{ageRange.CellingAge}]和已有时间段冲突.";

    }
}
