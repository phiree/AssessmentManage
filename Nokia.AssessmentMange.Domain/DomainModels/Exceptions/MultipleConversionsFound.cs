using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class MultipleConversionsFound:Exception
    {
        string subjectName;
        SexLimitation sexLimitation;
        int count;
        public MultipleConversionsFound(string subjectName,SexLimitation sexLimitation,int count)
        {   this.subjectName=subjectName;
            this.sexLimitation=sexLimitation;
            this.count=count;
            }
        public override string Message => $"科目[{subjectName}]的类型为[{sexLimitation}],应该只有一个成绩换算表,但有[{count}]个";
    }
}
