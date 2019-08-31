using Nokia.AssessmentMange.Domain.DomainModels;
 
using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IApplicationBase<T> where T : EntityBase
    { 
          void Create(T t);
      
            T Get(string id);
        IEnumerable<T> GetAll();
         void Update(T t);
        void  Delete(string id);
    }
}