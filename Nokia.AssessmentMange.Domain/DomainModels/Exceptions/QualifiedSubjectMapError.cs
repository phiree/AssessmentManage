using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class QualifiedSubjectMapError:Exception
    {
     
        int errorCount;
        public QualifiedSubjectMapError( int errorCount)
        {
           
            this.errorCount = errorCount;
            }
        public override string Message => $"是否合格类型 的换算条目有误.应该是2条,但是有[{errorCount}]条";
    }
}
