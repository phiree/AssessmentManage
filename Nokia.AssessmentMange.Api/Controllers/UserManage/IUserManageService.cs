using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Controllers.UserManage
{
    public interface IUserManagementService
    {
        bool IsValidUser(string username, string password);
    }
    public class UserManagementService : IUserManagementService
    {
        public bool IsValidUser(string userName, string password)
        {
            return true;
        }
    }
}
