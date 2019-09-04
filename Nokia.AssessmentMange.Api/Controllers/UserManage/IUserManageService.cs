using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.Common;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Controllers.UserManage
{
    public interface IUserManagementService
    {
        User IsValidUser(string username, string password);
    }
    public class UserManagementService : IUserManagementService
    {
        IUserApplication _userApplication;
        public UserManagementService(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }
        public User IsValidUser(string userName, string password)
        {
            password = CryptographyHelp.GetMD5(password);
            return _userApplication.GetUser(userName, password);
        }
    }
}
