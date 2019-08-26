using System.Collections.Generic;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public interface IGradeCalculater
    {
        void CalculateGrade(SubjectGrade subjectGrade, IList<SubjectGrade> subjectGrades);
    }
}