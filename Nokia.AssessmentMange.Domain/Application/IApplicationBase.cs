using Nokia.AssessmentMange.Domain.DomainModels.Entity;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IApplicationBase<T> where T : EntityBase
    {
        void Create(T newT);
        void Update(T newT);
    }
}