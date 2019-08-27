using System.Collections.Generic;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IAssessmentApplication
    {
        Assessment CreateAssessment(Assessment assessment);
        IEnumerable<Assessment> GetAllAssessment();
        PersonGrade GetPersonGrade(string assessmentId, string personId);
        PersonGrade SavePersonGrade(string assessmentId, string personId, bool isMakeup, IDictionary<string, double> grades);
    }
}