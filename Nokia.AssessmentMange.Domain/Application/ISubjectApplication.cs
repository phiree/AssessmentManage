using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface  ISubjectApplication:IApplicationBase<Subject>
    {
         Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, 
             bool isQualifiedConversion, string unit);


    }
}
