using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
   public interface ISubjectRepository:IEFCRepository<Subject>
    {
        Subject GetWithParamSubjects(string id);
    }
}
