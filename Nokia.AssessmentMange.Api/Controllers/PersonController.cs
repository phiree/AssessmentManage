﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Api.Controllers.Authentication;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.Common;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 人员管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {

        IPersonApplication _personApplication;
        IDepartmentApplication _departmentApplication;
        IUserApplication _userApplication;
        IMapper _mapper;
        public PersonController(IAuthenticateService authenticateService, IPersonApplication personApplication, IDepartmentApplication departmentApplication, IUserApplication userApplication, IMapper mapper) : base(authenticateService)
        {
            this._personApplication = personApplication;
            this._departmentApplication = departmentApplication;
            this._userApplication = userApplication;
            this._mapper = mapper;
        }

        /// <summary>
        /// 获取人员集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCurrent"></param>
        /// <returns></returns>
        [HttpGet("GetPersons")]
        public ActionResult<SearchPageVO<Person>> GetPersons(string deptID, string search, int? hasUser = null, int pageSize = 15, int pageCurrent = 1)
        {
            //根据claims获取用户所在部门,再获取考核
            User user = GetUserInfo();
            if (user == null)
            {
                return StatusCode(401);
            }
            return _personApplication.GetPersons(user, deptID, search, hasUser, pageSize, pageCurrent);
        }

        /// <summary>
        /// 创建人员
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="birthday">生日</param>
        /// <param name="sex"> 性别</param>
        /// <param name="position">职务</param>
        /// <param name="departmentId">部门</param>
        /// <param name="rank">军衔</param>
        /// <param name="idNo">证件号</param>
        [HttpPost("CreatePerson")]
        public Person CreatePerson([FromBody]PersonVO model)
        {
            Department department = null;
            if (!string.IsNullOrEmpty(model.DepartmentId))
            {
                department = _departmentApplication.Get(model.DepartmentId);
            }
            Person person = _mapper.Map<Person>(model);
            person.Department = department;
            _personApplication.Create(person);
            return person;
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
        public Person UpdatePerson([FromBody]PersonChangeVO model)
        {
            Department department = null;
            if (!string.IsNullOrEmpty(model.DepartmentId))
            {
                department = _departmentApplication.Get(model.DepartmentId);
            }
            User user = _userApplication.GetUserByPersonID(model.Id);

            Person person = _mapper.Map<Person>(model);
            person.Department = department;
            person.User = user;
            _personApplication.Update(person);
            return person;
        }
        /// <summary>
        /// 删除人员
        /// </summary>
        ///    <param name="personId"></param>
        [HttpGet("DeletePerson")]
        public bool DeletePerson(string personId)
        {
            return _personApplication.DeletePersons(personId);
        }
        /// <summary>
        /// 从人员创建登录账号
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="isAdmin"></param>
        [HttpGet("CreateUserForPerson")]
        public User CreateUserForPerson(string personId, string loginName, string password, bool isAdmin)
        {
            Person person = _personApplication.Get(personId);
            if (person == null)
            {
                throw new Exception("没有找到该用户");
            }
            //获取personId是否存在用户表
            User user = _userApplication.GetUserByPersonID(personId);
            if (user == null)
            {
                user = new User(loginName, isAdmin);
                user.Password = CryptographyHelp.GetMD5(password);
                user.PersonId = personId;
                _userApplication.Create(user);
            }
            else
            {
                user.PersonId = personId;
                user.LoginName = loginName;
                user.IsAdmin = isAdmin;
                user.Password = CryptographyHelp.GetMD5(password);
                _userApplication.Update(user);
            }
            user.Person = person;
            return user;
        }

        /// <summary>
        /// 修改人员关联的账号
        /// </summary>
        ///    <param name="personId"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        //[HttpPost("UpdateUserForPerson")]
        //public void UpdateUserForPerson(string personId, string loginName, string password)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 移除人员关联的账号
        /// </summary>
        ///  <param name="userID"></param>
        [HttpPost("RemoveUserForPerson")]
        public bool RemoveUserForPerson(string userID)
        {
            _userApplication.Delete(userID);
            return true;
        }


        /// <summary>
        /// 获取人员集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loginName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCurrent"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public SearchPageVO<User> GetUsers(string name, string loginName, int pageSize = 15, int pageCurrent = 1)
        {
            return _userApplication.GetUsers(name, loginName, pageSize, pageCurrent);
        }
    }
}