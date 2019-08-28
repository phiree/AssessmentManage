using Nokia.AssessmentMange.Domain.DomainModels.Entity;
using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IApplicationBase<T> where T : EntityBase
    {
        T Get(string id);
        IEnumerable<T> GetAll();
         void Update(T t);
    }
}