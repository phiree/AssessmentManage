using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class SubjectApplication : ApplicationBase<Subject>, ISubjectApplication
    {
        public SubjectApplication(ISubjectRepository subjectRepository)
            : base(subjectRepository)
        {

        }

    }
}
