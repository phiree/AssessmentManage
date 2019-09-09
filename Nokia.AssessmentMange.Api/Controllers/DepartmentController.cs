using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Api.Controllers.Authentication;
using Nokia.AssessmentMange.Domain.Common;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        IDepartmentApplication departmentApplication;
        public DepartmentController(IDepartmentApplication departmentApplication, IAuthenticateService authenticateService) : base(authenticateService)
        {
            this.departmentApplication = departmentApplication;
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public ActionResult<List<Department>> GetAll()
        {
            //根据claims获取用户所在部门,再获取考核
            User user = GetUserInfo();
            if (user == null)
            {
                return StatusCode(401);
            }
            return departmentApplication.GetWithAllChildren(user);
        }
        /// <summary>
        /// 根据用户获取部门
        /// 管理员看所有，非管理员看本级以及下级
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllByUser")]
        public ActionResult<List<Department>> GetAllByUser()
        {
            User user = GetUserInfo();
            if (user == null)
            {
                return StatusCode(401);
            }
            return departmentApplication.GetWithAllChildrenByUser(user);
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public ActionResult<Department> Get(string departmentId)
        {
            User user = GetUserInfo();
            if (user == null)
            {
                return StatusCode(401);
            }
            return departmentApplication.GetWithSingleChildren(departmentId, user);
        }
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public Department Create([FromBody]DepartmentVO model)
        {
            Department parent = null;
            if (!string.IsNullOrEmpty(model.ParentId))
            {
                parent = departmentApplication.Get(model.ParentId);
            }
            Department department = new Department(model.Name, parent);
            departmentApplication.Create(department);
            return department;
        }
        /// <summary>
        /// 更新部门
        /// </summary>
        /// <returns></returns>
        [HttpPost("Update")]
        public Department Update([FromBody]DepartmentChangeVO model)
        {
            Department parent = null;
            if (!string.IsNullOrEmpty(model.ParentId))
            {
                parent = departmentApplication.Get(model.ParentId);
            }
            Department department = new Department(model.Name, parent);
            department.Id = model.ID;
            departmentApplication.Update(department);
            return department;
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId"></param>
        [HttpGet("Delete")]
        public ResultModel<bool> Delete(string departmentId)
        {
            return departmentApplication.DeleteDepartment(departmentId);
        }

    }
}