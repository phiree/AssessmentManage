using Nokia.AssessmentMange.Domain.DomainModels.Entity;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IApplicationBase<T> where T : EntityBase
    {
        T Get(string id);
        
    }
}