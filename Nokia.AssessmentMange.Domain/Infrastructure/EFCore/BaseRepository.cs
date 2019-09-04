using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class EFCRepository<T> : IEFCRepository<T> where T : DomainModels.EntityBase
    {

        protected AssessmentDbContext Conn { get; private set; }
        public EFCRepository(AssessmentDbContext conn)
        {
            this.Conn = conn;

        }

        public T Get(string id)
        {
            return Conn.Set<T>()

               .Find(id);

            ;
            return Conn.Set<T>()

                .Include(Conn.GetIncludePaths(typeof(T)))
                .First(x => x.Id == id);

        }
        /// <summary>
        /// 贪婪加载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEager(string id)
        {

            ;
            return Conn.Set<T>()

                .Include(Conn.GetIncludePaths(typeof(T)))
                .First(x => x.Id == id)

                ;





        }
        public IEnumerable<T> GetList(IEnumerable<string> idList)
        {
            return idList.Select(x => Get(x)).ToList();
        }
        public bool Delete(T obj)
        {
            try
            {
                Conn.Remove(obj);
                Conn.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }


        public bool Delete(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException("Too dangrouse to implement");
        }

        public IEnumerable<T> GetAll()
        {
            return Conn.Set<T>();

        }

        public void Insert(T obj, IDbTransaction transaction = null)
        {
            Conn.Add(obj);

            Conn.SaveChanges();

        }

        public void Insert(IEnumerable<T> list)
        {
            Conn.AddRange(list);
            Conn.SaveChanges();
            // return Conn.Insert<IEnumerable<T>>(list);

        }

        public bool Update(T obj)
        {
            Conn.Update(obj);
            Conn.SaveChanges();

            return true;



        }
        public void SaveChanges()
        {
            Conn.SaveChanges();
        }

        public bool Update(IEnumerable<T> list)
        {

            Conn.UpdateRange(list);
            return true;

        }

        public IEnumerable<T> Search(IDictionary<string, object> param)
        {

            throw new NotImplementedException();
        }
        public IEnumerable<T> Search(string sql, IDictionary<string, object> param)
        {
            return Conn.Set<T>().FromSql(sql, param);
            //throw new NotImplementedException();
        }
        public IEnumerable<T> Search(string sql, dynamic param, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Conn.Database.BeginTransaction();
        }

        public IEnumerable<T> SearchWithPage(IDictionary<string, object> param, int pageIndex, int pageSize)
        {
            return Search(param).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<T> SearchWithPage(string sql, dynamic param, int pageIndex, int pageSize)
        {
            return Search(sql, param).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public T FindOne(Expression<Func<T, bool>> where)
        {
            return Conn.Set<T>().SingleOrDefault(where);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return Conn.Set<T>().Where(where);
        }

        public void Delete(string id)
        {
            Conn.Set<T>().Remove(Get(id));
            Conn.SaveChanges();
        }

        public List<T> SearchWithPage(Expression<Func<T, bool>> where, int pageIndex, int pageSize, out int rowCount)
        {
            var rowList = Conn.Set<T>().Where(where);
            rowCount = rowList.Count();
            return rowList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }

}
