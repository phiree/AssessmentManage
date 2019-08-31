using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class ConversionNotFound:Exception
    {
        Sex sex; int age;string subjectName;
        public ConversionNotFound(Sex sex, string subjectName)
        { 
            this.sex=sex;
            
            this.subjectName=subjectName;
            }
        public override string Message => $"没有找到科目[{subjectName}]对应性别[{sex.ToString()}]的成绩换算表 ";
    }
}
