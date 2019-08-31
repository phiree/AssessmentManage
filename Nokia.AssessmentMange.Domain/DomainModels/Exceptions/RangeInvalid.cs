using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class RangeInvalid<T>:Exception where T:IComparable<T>
    {
       T min,max;
        public RangeInvalid(T min,T max)
        { 
            this.min=min;
            this.max=max;
            
            }
        public override string Message =>$"Range错误,最小值[{min}]不应该大于最大值[{max}]";

    }
}
