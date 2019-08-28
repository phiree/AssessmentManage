using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Entity;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class SubjectRepository:EFCRepository<Subject>,ISubjectRepository
    {
        public SubjectRepository(AssessmentDbContext dbContext):base(dbContext)
        { }
    }
}
