using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class ApplicationBase<T> : IApplicationBase<T> where T :  EntityBase
    {
        protected IRepository<T> baseRepository { get;private set;}
        public ApplicationBase(IRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;

        }
        /// <summary>
        /// 反模式.
        /// 参数t 意味着 application需要负责对象的创建. 不要滥用此方法.
        /// </summary>
        /// <param name="t"></param>
        public void Create(T t) { 
            baseRepository.Insert(t);

            }
        public T Get(string id)
        { 
            return baseRepository.Get(id);
            }
        public IEnumerable<T> GetAll()
        { 
            return baseRepository.GetAll();
            }
        public void Update(T t)
        {
          baseRepository.Update(t);
        }
        public void Delete(string id)
        {
            baseRepository.Delete(id);
        }

    }
}
