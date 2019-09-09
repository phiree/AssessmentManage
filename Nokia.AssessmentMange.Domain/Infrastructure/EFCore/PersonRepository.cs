using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class PersonRepository : EFCRepository<Person>, IPersonRepository
    {
        public PersonRepository(AssessmentDbContext conn) : base(conn) { }

        public int GetPersonCountByDepartment(string deptID)
        {
            return Conn.Person.Count(item => item.DepartmentId == deptID);
        }

        public Person GetPersonByUser(string uid)
        {
            return Conn.Person.Join(Conn.Users.Where(item => item.Id == uid), person => person.Id, user => user.PersonId, (a, b) => new Person()
            {
                Id = a.Id,
                DepartmentId = a.DepartmentId
            }).FirstOrDefault();
        }

        public List<Person> GetPersons(string deptID, string search, int? hasUser, int pageSize, int pageCurrent, out int rowCount)
        {
            var where = PredicateBuilder.True<Person>();
            where = where.And(p => p.State == 1);
            if (!string.IsNullOrEmpty(deptID))
            {
                where = where.And(p => p.DepartmentId == deptID);
            }
            if (!string.IsNullOrEmpty(search))
            {
                where = where.And(p => p.RealName.Contains(search) || p.IdNo.Contains(search));
            }
            var selectSet = Conn.Person;
            if (hasUser.HasValue)
            {
                if (hasUser.Value == 0)
                {
                    where = where.And(item => item.User == null);
                }
                else if (hasUser.Value == 1)
                {
                    where = where.And(item => item.User != null);
                }
                selectSet.Include(i => i.User);
            }
            var result = selectSet.Where(where);
            rowCount = result.Count(where);
            if (!hasUser.HasValue)
            {
                result = result.Include(i => i.User);
            }
            return result.Include(i => i.Department).Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
