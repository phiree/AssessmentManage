using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.Common;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class DepartmentApplication : ApplicationBase<Department>, IDepartmentApplication
    {
        IDepartmentRepository _departmentRepository;
        IUserRepository _userRepository;
        IPersonRepository _personrepository;
        public DepartmentApplication(IDepartmentRepository departmentRepository, IUserRepository userRepository, IPersonRepository personrepository)
            : base(departmentRepository)
        {
            this._departmentRepository = departmentRepository;
            this._userRepository = userRepository;
            this._personrepository = personrepository;
        }

        public Department Create(string name, Department parent)
        {
            throw new NotImplementedException();
        }
        public List<Department> GetWithAllChildren(User user)
        {
            return _departmentRepository.GetWithAllChildren(user.IsAdmin);
        }

        public Department GetWithSingleChildren(string id, User user)
        {
            var department = _departmentRepository.GetWithSingleChildren(id, user.IsAdmin);
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
            return _departmentRepository.GetWithAllChildren(user.Person.DepartmentId, user.IsAdmin);
        }

        public ResultModel<bool> DeleteDepartment(string id)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            //判断部门下是否有部门
            int dCount = _departmentRepository.GetChildrenByID(id);
            if (dCount > 0)
            {
                result.Code = ResultCode.error;
                result.Data = false;
                result.Message = "请先移除该部门下所有子部门";
                return result;
            }
            //判断是否有用户
            int pCount = _personrepository.GetPersonCountByDepartment(id);
            if (pCount > 0)
            {
                result.Code = ResultCode.error;
                result.Data = false;
                result.Message = "请先移除该部门下所有用户";
                return result;
            }
            Department department = _departmentRepository.Get(id);
            department.State = 0;
            result.Code = ResultCode.success;
            result.Data = _departmentRepository.Update(department);
            return result;
        }
    }
}
