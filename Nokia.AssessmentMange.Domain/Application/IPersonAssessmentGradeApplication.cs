using System.Collections.Generic;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IPersonAssessmentGradeApplication
    {
        PersonAssessmentGrade CommitGrades(string personAssessmentGradeId, bool isAbsent, bool isMakeup, IList<SubjectGradeModel> subjectGradeModels);
        PersonAssessmentGrade Get(string assessmentId, string personId);
    }
}