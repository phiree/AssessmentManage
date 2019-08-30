using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class AgeRangeError:Exception
    {
       int cellingAge,floorAge;
        public AgeRangeError(int cellingAge,int floorAge)
        { 
            this.cellingAge=cellingAge;
            this.floorAge=floorAge;
            
            }
        public override string Message =>$"年龄段错误,最小年龄[{floorAge}]不应该大于最大年龄[{cellingAge}]";

    }
}
