using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Application;
namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
       ApplicationBase<Department> departmentApplication;
        public DepartmentController(ApplicationBase<Department> departmentApplication)
        { 
            this.departmentApplication=departmentApplication;
            }
        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost("Get")]
        public Department Get(string departmentId)
        { 
            return departmentApplication.Get(departmentId);
            }
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public Department Create(string name,string parentId)
        { 
            Department department=new Department(name,parentId);
           departmentApplication.Create(department);
            return department;
            }
        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public Department Update(string name, string parentId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        [HttpPost("Delete")]
        public void Delete(string name, string parentId)
        {
            throw new NotImplementedException();
        }

    }
}