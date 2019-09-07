using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class DepartmentApplication : ApplicationBase<Department>, IDepartmentApplication
    {
        IDepartmentRepository _departmentRepository;
        IUserRepository _userRepository;

        public DepartmentApplication(IDepartmentRepository departmentRepository, IUserRepository userRepository)
            : base(departmentRepository)
        {
            this._departmentRepository = departmentRepository;
            this._userRepository = userRepository;
        }

        public Department Create(string name, Department parent)
        {
            throw new NotImplementedException();
        }
        public List<Department> GetWithAllChildren(User user)
        {
            return _departmentRepository.GetWithAllChildren(user.IsAdmin);
        }

        public Department GetWithSingleChildren(string id)
        {
            var department = _departmentRepository.GetWithSingleChildren(id);
            return department;
        }

        public List<Department> GetWithAllChildrenByUser(User user)
        {
            //首先获取用户
            //User user = _userRepository.GetUser(userID);
            if (user.IsAdmin)
            {
                return GetWithAllChildren(user);
            }
            //根据用户获取本部门及下级部门
            return _departmentRepository.GetWithAllChildren(user.Person.DepartmentId);
        }

        public bool DeleteDepartment(string id)
        {
            Department department = _departmentRepository.Get(id);
            department.State = 0;
            return _departmentRepository.Update(department);
        }
    }
}
