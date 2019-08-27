using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public interface IAssessmentService
    {
        void SavePersonGrade(Assessment assessment, Person person, bool isAbsent, bool isMakeup, IEnumerable<SubjectGrade> subjectGrades);
    }
}