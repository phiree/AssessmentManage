using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.Application.Impl
{
    public class UserApplication : ApplicationBase<User>, IUserApplication
    {
        IUserRepository _userRepository;
        IRepository<Department> _departmentRepository;
        public UserApplication(IUserRepository userRepository, IRepository<Department> departmentRepository) : base(userRepository)
        {
            this._userRepository = userRepository;
            this._departmentRepository = departmentRepository;
        }


        public User GetUser(string loginName, string passWord)
        {
            User user = _userRepository.GetUser(loginName, passWord);
            return user;
        }

        public User GetUserByPersonID(string personID)
        {
            User user = _userRepository.FindOne(item => item.PersonId == personID);
            return user;
        }

        public SearchPageVO<User> GetUsers(string name, string loginName, int pageSize, int pageCurrent)
        {
            SearchPageVO<User> result = new SearchPageVO<User>();
            int pageCount = 0;
            List<User> usersList = _userRepository.GetUsers(name, loginName, pageSize, pageCurrent, out pageCount);
            if (pageCount != 0)
            {
                IEnumerable<string> deptIDList = usersList.Select(item => item.Person).Select(item => item.DepartmentId).Distinct();
                IEnumerable<Department> deptList = _departmentRepository.GetList(deptIDList);
                foreach (var u in usersList)
                {
                    u.Person.Department = deptList.FirstOrDefault(item => item.Id == u.Person.DepartmentId);
                }
            }
            result.PageCurrent = pageCurrent;
            result.PageSize = pageSize;
            result.DataList = usersList;
            result.RowCount = pageCount;
            return result;
        }

        public IEnumerable<User> GetUsers(IEnumerable<string> persionIDs)
        {
            return _userRepository.Find(item => persionIDs.Contains(item.Id));
        }
    }
}
