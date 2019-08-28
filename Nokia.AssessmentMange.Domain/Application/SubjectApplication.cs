using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using Nokia.AssessmentMange.Domain.DomainModels.Service;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class SubjectApplication : ApplicationBase<Subject>, ISubjectApplication
    {
        ISubjectService subjectService;
        public SubjectApplication(ISubjectRepository subjectRepository, ISubjectService subjectService)
            : base(subjectRepository)
        {
            this.subjectService=subjectService;
        }
        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        { 
           return subjectService.Create(  name,   subjectType,   sexLimitation,   isQualifiedConversion,   unit);
        }

    }
}
