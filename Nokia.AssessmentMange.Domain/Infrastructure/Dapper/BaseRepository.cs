using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Data;
using Nokia.AssessmentMange.Domain.DomainModels.Entity;
using System.Data.SqlClient;
 
using Dapper;
using Dapper.Contrib;
using Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure;
using Dapper.Contrib.Extensions;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;

namespace Nokia.AssessmentMange.Domain.Infrastructure.Repository.Dapper
{


    public class BaseRepository<T> : IRepository<T> where T : class
    {
        string connectionString;
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;

        }
        IDbConnection conn;


        protected IDbConnection Conn
        {
            get
            {
                conn = conn ?? new SqlConnection(connectionString);

                return conn;
            }
        }
        protected IDbConnection GetOpenedConn()
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            return Conn;
        }
        public T Get(string id)
        {
            return Conn.Get<T>(id);
            throw new NotImplementedException();
        }
        public IEnumerable<T> GetList(IEnumerable<string> idList)
        {
            return idList.Select(x => Get(x)).ToList();
        }
        public bool Delete(T obj)
        {
            return Conn.Delete(obj);

        }
        public T GetByAttributeId(object explictId)
        {
            return Conn.Get<T>(explictId);// Conn.Get<T>(explictId);
        }

        public bool Delete(IEnumerable<T> list)
        {
            return Conn.Delete(list);
        }

        public bool DeleteAll()
        {

            throw new NotImplementedException("Too dangrouse to implement");
        }

        public IEnumerable<T> GetAll()
        {
            return Conn.GetAll<T>();
            throw new NotImplementedException();
        }

        public long Insert(T obj, IDbTransaction transaction = null)
        {
            return Conn.Insert(obj, transaction: transaction);
            throw new NotImplementedException();
        }

        public long Insert(IEnumerable<T> list)
        {
            return Conn.Insert<IEnumerable<T>>(list);
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            return conn.Update(obj);

        }

        public bool Update(IEnumerable<T> list)
        {
            return conn.Update(list);
            throw new NotImplementedException();
        }

        public IEnumerable<T> Search(IDictionary<string, object> param)
        {
            DynamicParameters dp = new DynamicParameters(param);
            string sql = new SqlBuilderForGeneric<T>().BuildQuery(dp);
            return Conn.Query<T>(sql, dp);
        }
        public IEnumerable<T> Search(string sql, dynamic param)
        {
            DynamicParameters dp = new DynamicParameters(param);

            return Conn.Query<T>(sql, dp);
        }
        public T FindOne(IDictionary<string, object> param)
        {
            DynamicParameters dp = new DynamicParameters(param);
            string sql = new SqlBuilderForGeneric<T>().BuildQuery(dp);
            var result = Conn.Query<T>(sql, dp);
            if (result.Count() == 1)
            {
                return result.First();
            }
            else if (result.Count() == 0)
            {
                throw new NotFoundInRepository(param, typeof(T));
            }
            else
            {
                throw new MultiFoundInRepository(param, typeof(T));
            }

        }
        public IEnumerable<T> Search(string sql, dynamic param, int page, int itemsPerPage)
        {
            DynamicParameters dp = new DynamicParameters(param);
            return Conn.Query<T>(sql, dp).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        }

        public IDbTransaction BeginTransaction()
        {
            return GetOpenedConn().BeginTransaction();
        }

        public IEnumerable<T> SearchWithPage(IDictionary<string, object> param, int pageIndex, int pageSize)
        {
            return Search(param).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<T> SearchWithPage(string sql, dynamic param, int pageIndex, int pageSize)
        {
            return Search(sql, param).Skip(pageIndex * pageSize).Take(pageSize);
        }
    }


}

