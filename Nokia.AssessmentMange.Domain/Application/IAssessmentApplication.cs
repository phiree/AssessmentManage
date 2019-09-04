using System.Collections.Generic;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IAssessmentApplication:IApplicationBase<Assessment>
    {
        Assessment CreateAssessment(AssessmentCreateModel assessment);
        IEnumerable<Assessment> GetAllAssessment();
        PersonAssessmentGrade GetPersonGrade(string assessmentId, string personId);
        PersonAssessmentGrade SavePersonGrade(string assessmentId, string personId, bool isMakeup, IDictionary<string, double> grades);

        Assessment UpdateSubjects(AssessmentModel assessmentModel);
    }
}