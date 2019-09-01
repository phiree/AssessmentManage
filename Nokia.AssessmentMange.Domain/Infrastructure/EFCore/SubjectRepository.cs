using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Nokia.AssessmentMange.Domain.DomainModels;
 
using Nokia.AssessmentMange.Domain.DomainModels.Repository;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class SubjectRepository:EFCRepository<Subject>,ISubjectRepository
    {
        public SubjectRepository(AssessmentDbContext dbContext):base(dbContext)
        { }

        public Subject GetWithParamSubjects(string id)
        {
            return Conn.ComputedSubjects
                .Include(x=>x.ParamSubjects)
                    .ThenInclude(p=>p.PSubject)
                .First(x=>x.Id==id);
        }
    }
}
