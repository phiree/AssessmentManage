using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper
{
    public interface IPersonAssessementGradeMapper
    {
        PersonAssessmentGrade ToEntity(PersonAssessementGradeCreateModel createModel);
    }
}