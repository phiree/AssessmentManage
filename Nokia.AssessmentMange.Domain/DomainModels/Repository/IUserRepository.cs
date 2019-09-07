using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IUserRepository : IEFCRepository<User>
    {
        List<User> GetUsers(string name, string loginName, int pageSize, int pageCurrent, out int rowCount);
        User GetUserByPerson(string personID);
        User GetUser(string id);
        User GetUser(string loginName, string passWord);
    }
}
