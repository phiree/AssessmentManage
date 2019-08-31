using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public abstract class Score<T> {
      public   static Score<T> NullValue { get;}
        }
    /// <summary>
    /// 得分
    /// </summary>
    public class NumericalScore:Score<double?>
    { 
        public double Value { get;set;}
        public NumericalScore(double? value)
        { 
            
            }
        }
    
}
