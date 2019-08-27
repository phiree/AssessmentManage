using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Data;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFC
{
    public class BaseRepository<T> : IRepositoryEFC<T> where T :DomainModels.Entity.EntityBase 
    {
        
        AssessmentDbContext Conn;
        public BaseRepository(AssessmentDbContext conn)
        {
            this.Conn = conn;

        }
       
        public T Get(string id)
        {
            return Conn.Set<T>().First(x=>x.Id==id);
            
        }
        public IEnumerable<T> GetList(IEnumerable<string> idList)
        {
            return idList.Select(x => Get(x)).ToList();
        }
        public bool Delete(T obj)
        {
            try{ Conn.Remove(obj);
                return true;
                }
            catch { 
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
            return Conn.Set <T>() ;
            
        }

        public long Insert(T obj, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
            
           
        }

        public long Insert(IEnumerable<T> list)
        {
           // return Conn.Insert<IEnumerable<T>>(list);
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            try{ Conn.Update(obj);
                
                return true;}
            catch { 
                return false;
                }
             

        }

        public bool Update(IEnumerable<T> list)
        {
            try { 
              Conn.UpdateRange(list);
                return true;
            }
            catch
            { 
                return false;
                }
            throw new NotImplementedException();
        }

        public IEnumerable<T> Search(IDictionary<string, object> param)
        {
          throw new NotImplementedException();
        }
        public IEnumerable<T> Search(string sql, dynamic param)
        {
            throw new NotImplementedException();
        }
        public T FindOne(IDictionary<string, object> param)
        {
            throw new NotImplementedException();

        }
        public IEnumerable<T> Search(string sql, dynamic param, int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
           throw new NotImplementedException();
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
            return Conn.Set<T>().Single(where);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return Conn.Set<T>().Where(where);
        }
    }

}
