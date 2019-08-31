using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class CalculateScoreError:Exception
    {
        string subjectName,errMsg,personName;
       
        
        public CalculateScoreError(string subjectName,string personName,    string errMsg)
        { 
            this.subjectName=subjectName;
            this.errMsg=errMsg;
            this.personName=personName;
            
            }
        public override string Message => $"用户[{personName}]科目[{subjectName}]得分计算错误.详情:{errMsg}";

    }
}
