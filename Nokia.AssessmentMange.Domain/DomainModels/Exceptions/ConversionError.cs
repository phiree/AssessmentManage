using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public  class ConversionError:Exception
    {
        string subjectName, personName,errMsg;Sex sex;
        public ConversionError(string subjectName,string personName, Sex sex,string errMsg)
        {
            this.subjectName=subjectName;
            this.personName=personName;
            this.sex=sex;
            this.errMsg=errMsg;
            }
        public override string Message =>$"用户[{personName},{sex}]获取科目[{subjectName}]时发生错误.详情:{errMsg}";
    }
}
