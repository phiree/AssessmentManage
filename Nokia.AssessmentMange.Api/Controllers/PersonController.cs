using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 人员管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        /// <summary>
        /// 创建人员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="birthday"></param>
        /// <param name="sex"></param>
        /// <param name="position">职务</param>
        /// <param name="departmentId"></param>
        /// <param name="rank">军衔</param>
        /// <param name="idNo">证件号</param>
        [HttpPost("CreatePerson")]
        public Person CreatePerson(string name,DateTime birthday,Sex sex,
            string position, string departmentId, MilitaryRank rank,string idNo)
        { 
            throw new NotImplementedException();
            }
        /// <summary>
        /// 修改人员
        /// </summary>
        ///    <param name="personId"></param>
        /// <param name="name"></param>
        /// <param name="birthday"></param>
        /// <param name="sex"></param>
        /// <param name="position">职务</param>
        /// <param name="departmentId"></param>
        /// <param name="rank">军衔</param>
        /// <param name="idNo">证件号</param>
        [HttpPost("UpdatePerson")]
        public Person UpdatePerson(string personId, string name, DateTime birthday, Sex sex,
            string position, string departmentId, MilitaryRank rank, string idNo)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除人员
        /// </summary>
        ///    <param name="personId"></param>
        [HttpPost("DeletePerson")]
        public Person DeletePerson(string personId )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 从人员创建登录账号
        /// </summary>
        ///    <param name="personId"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        [HttpPost("CreateUserForPerson")]
        public void CreateUserForPerson(string personId, string loginName,string password)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改人员关联的账号
        /// </summary>
        ///    <param name="personId"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        [HttpPost("UpdateUserForPerson")]
        public void UpdateUserForPerson(string personId,string loginName,string password)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 移除人员关联的账号
        /// </summary>
        ///    <param name="personId"></param>
        [HttpPost("RemoveUserForPerson")]
        public void RemoveUserForPerson(string personId )
        {
            throw new NotImplementedException();
        }
    }
}