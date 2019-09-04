using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IUserApplication : IApplicationBase<User>
    {
        IEnumerable<User> GetUsers(IEnumerable<string> persionIDs);


        User GetUser(string loginName, string passWord);
        User GetUserByPersonID(string personID);
        UserSearchVO GetUsers(string name, string loginName, int pageSize, int pageCurrent);
    }
}
