using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class ScoreAlreadyExisted:Exception
    {
        double score;
        public ScoreAlreadyExisted(double score)
        { this.score= score;
            
            }
        public override string Message =>$"分数值[{score}]已经存在";

    }
}
