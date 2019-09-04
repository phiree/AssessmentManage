using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IRepository<T> : IRepository<T, string> { }
    public interface IRepository<T, Key>
    {
        IEnumerable<T> GetList(IEnumerable<Key> idList);
        T Get(Key id);
        T FindOne(Expression<Func<T, bool>> where);
        /// <summary>
        /// 针对于该类型的简单查询
        /// </summary>
        /// <param name="param">查询条件,key值必须等于列名</param>
        /// <returns></returns>
        IEnumerable<T> SearchWithPage(IDictionary<string, object> param, int pageIndex, int pageSize);
        IEnumerable<T> Search(IDictionary<string, object> param);
        /// <summary>
        /// 涉及到其他类型,或者复杂的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> Search(string sql, IDictionary<string, object> param);
        IEnumerable<T> SearchWithPage(string sql, dynamic param, int pageIndex, int pageSize);
        IEnumerable<T> GetAll();
        void Insert(T obj, IDbTransaction transaction = null);
        void Insert(IEnumerable<T> list);
        bool Update(T obj);
        bool Update(IEnumerable<T> list);
        bool Delete(T obj);
        void Delete(Key id);
        bool Delete(IEnumerable<T> list);
        bool DeleteAll();

        IDbContextTransaction BeginTransaction();


        List<T> SearchWithPage(Expression<Func<T, bool>> where, int pageIndex, int pageSize, out int rowCount);

    }


    public interface IEFCRepository<T> : IRepository<T>
    {
        T GetEager(string id);
        T FindOne(Expression<Func<T, bool>> where);
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        void SaveChanges();

    }
}
