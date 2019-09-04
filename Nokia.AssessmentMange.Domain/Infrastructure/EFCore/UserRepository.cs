using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.Common;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class UserRepository : EFCRepository<User>, IUserRepository
    {
        public UserRepository(AssessmentDbContext conn) : base(conn) { }

        public User GetUserByPerson(string personID)
        {
            return Conn.Users.FirstOrDefault(item => item.PersonId == personID);
        }

        public List<User> GetUsers(string name, string loginName, int pageSize, int pageCurrent, out int rowCount)
        {
            var where = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(loginName))
            {
                where = where.And(u => u.LoginName.Contains(loginName));
            }

            var pwhere = PredicateBuilder.True<Person>();
            if (!string.IsNullOrEmpty(name))
            {
                pwhere = pwhere.And(p => p.RealName.Contains(name));
            }
            var list = Conn.Users.Where(where)
                  .Join(Conn.Person.Where(pwhere), user => user.PersonId, person => person.Id, (user, person) => new User
                  {
                      Id = user.Id,
                      LoginName = user.LoginName,
                      PersonId = person.Id,
                      Person = new Person()
                      {
                          Id = person.Id,
                          RealName = person.RealName,
                          DepartmentId = person.DepartmentId
                      }
                  });

            rowCount = list.Count();
            return list.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

        }
    }
}
