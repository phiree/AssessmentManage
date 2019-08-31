using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class AgeConversionNotFound:Exception
    {
        int age;
        
            
        public AgeConversionNotFound(int age)
        { 
            this.age=age;
             
          
            }
        public override string Message =>$"没有年龄[{age}]的换算项";
    }
}
