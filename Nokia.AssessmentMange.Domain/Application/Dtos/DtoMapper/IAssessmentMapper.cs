using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper
{
    public interface IAssessmentMapper
    {
        Assessment ToEntity(AssessmentModel model);
    }
}