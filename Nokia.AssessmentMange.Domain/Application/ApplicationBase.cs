using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class ApplicationBase<T> : IApplicationBase<T> where T : DomainModels.Entity.EntityBase
    {
        IRepository<T> baseRepository;
        public ApplicationBase(IRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;

        }
        public void Create(T newT)
        {
            baseRepository.Insert(newT);

        }
        public void Update(T newT)
        {
            baseRepository.Update(newT);
        }

    }
}
