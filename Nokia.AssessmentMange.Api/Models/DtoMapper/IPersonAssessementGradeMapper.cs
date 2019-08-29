using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Api.Models.DtoMapper
{
    public interface IPersonAssessementGradeMapper
    {
        PersonAssessmentGrade ToEntity(PersonAssessementGradeCreateModel createModel);
    }
}