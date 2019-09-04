using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Api.Models;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentApplication departmentApplication;
        public DepartmentController(IDepartmentApplication departmentApplication)
        {
            this.departmentApplication = departmentApplication;
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public List<Department> GetAll()
        {
            return departmentApplication.GetWithAllChildren();
        }


        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public Department Get(string departmentId)
        {
            return departmentApplication.GetWithAllChildren(departmentId);
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
        public Department Update([FromBody]DepartmentVO model)
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
        public bool Delete(string departmentId)
        {
            departmentApplication.Delete(departmentId);
            return true;
        }

    }
}