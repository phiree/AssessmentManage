 
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface ISubjectConversionRepository : IRepository<SubjectConversion>
    {
        IList<SubjectConversion> GetSubjectConversions(string subjectId);
    }
}
