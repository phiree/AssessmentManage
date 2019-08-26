using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList(IEnumerable<object> idList);
        T Get(object id);
        /// <summary>
        /// 针对于该类型的简单查询
        /// </summary>
        /// <param name="param">查询条件,key值必须等于列名</param>
        /// <returns></returns>
        IEnumerable<T> Search(IDictionary<string, object> param);
        /// <summary>
        /// 涉及到其他类型,或者复杂的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> Search(string sql, dynamic param);
        IEnumerable<T> GetAll();
        long Insert(T obj, IDbTransaction transaction = null);
        long Insert(IEnumerable<T> list);
        bool Update(T obj);
        bool Update(IEnumerable<T> list);
        bool Delete(T obj);
        bool Delete(IEnumerable<T> list);
        bool DeleteAll();

        IDbTransaction BeginTransaction();
    }
}
