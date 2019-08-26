using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class QualifiedSubjectMapError:Exception
    {
        string subjectName;
        int errorCount;
        public QualifiedSubjectMapError(string subjectName,int errorCount)
        {
            this.subjectName=subjectName;
            this.errorCount = errorCount;
            }
        public override string Message => $"是否合格类型的科目[{subjectName}]的换算条目有误.应该是2条,但是有[{errorCount}]条";
    }
}
