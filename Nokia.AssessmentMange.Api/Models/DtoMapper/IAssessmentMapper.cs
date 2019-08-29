using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Api.Models.DtoMapper
{
    public interface IAssessmentMapper
    {
        Assessment ToEntity(AssessmentModel model);
    }
}