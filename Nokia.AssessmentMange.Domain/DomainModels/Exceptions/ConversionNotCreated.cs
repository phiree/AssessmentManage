using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class ConversionNotCreated:Exception
    {
         string subjectName;
        public ConversionNotCreated( string subjectName)
        {  
            this.subjectName=subjectName;
            }
        public override string Message => $"尚未创建科目[{subjectName}]成绩换算表 )";
    }
}
