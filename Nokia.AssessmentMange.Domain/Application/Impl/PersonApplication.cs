﻿using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.Common;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace Nokia.AssessmentMange.Domain.Application.Impl
{
    public class PersonApplication : ApplicationBase<Person>, IPersonApplication
    {
        IRepository<Department> _departmentRepository;
        IUserRepository _userRepository;
        IRepository<Person> _personRepository;
        IPersonRepository _repository;
        public PersonApplication(IPersonRepository repository, IRepository<Person> personRepository, IRepository<Department> departmentRepository, IUserRepository userRepository) : base(personRepository)
        {
            this._personRepository = personRepository;
            this._departmentRepository = departmentRepository;
            this._userRepository = userRepository;
            this._repository = repository;
        }

        public bool DeletePersons(string personId)
        {
            bool result = false;
            //using (var tran = _personRepository.BeginTransaction())
            //{
            //    try
            //    {
            //        _personRepository.Delete(_personRepository.Get(personId));
            //        _userRepository.Delete(_userRepository.GetUserByPerson(personId));
            //        tran.Commit();
            //        result = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        tran.Rollback();
            //        result = false;
            //    }
            //}
            Person person = _personRepository.Get(personId);
            person.State = 0;
            _personRepository.Update(person);
            return result;
        }

        public SearchPageVO<Person> GetPersons(User user, string deptID, string name, string idNo, int pageSize, int pageCurrent)
        {
            if (string.IsNullOrEmpty(deptID) && !user.IsAdmin)
            {
                //获取登录用户的部门
                Person person = _repository.GetPersonByUser(user.Id);
                if (person != null)
                {
                    deptID = person.DepartmentId;
                }
            }

            SearchPageVO<Person> result = new SearchPageVO<Person>();
            int pageCount = 0;
            var where = PredicateBuilder.True<Person>();
            where = where.And(p => p.State == 1);
            if (!string.IsNullOrEmpty(deptID))
            {
                where = where.And(p => p.DepartmentId == deptID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(p => p.RealName.Contains(name));
            }

            if (!string.IsNullOrEmpty(idNo))
            {
                where = where.And(p => p.IdNo.Contains(idNo));
            }
            var list = _personRepository.SearchWithPage(where, pageCurrent, pageSize, out pageCount);

            //获取部门
            if (pageCount >= 1)
            {
                //获取部门
                IEnumerable<string> deptIDList = list.Select(item => item.DepartmentId).Distinct();
                IEnumerable<Department> deptList = _departmentRepository.GetList(deptIDList);
                //获取登陆账号
                IEnumerable<string> personIDList = list.Select(item => item.Id).Distinct();
                IEnumerable<User> userList = _userRepository.Find(item => personIDList.Contains(item.Id));

                foreach (var person in list)
                {
                    person.Department = deptList.FirstOrDefault(item => item.Id == person.DepartmentId);
                    person.User = userList.FirstOrDefault(item => item.PersonId == person.Id);
                }
            }

            result.PageCurrent = pageCurrent;
            result.PageSize = pageSize;
            result.DataList = list;
            result.RowCount = pageCount;

            return result;
        }

        public SearchPageVO<Person> GetPersons(User user, string deptID, string search, int? hasUser, int pageSize, int pageCurrent)
        {
            if (string.IsNullOrEmpty(deptID) && !user.IsAdmin)
            {
                //获取登录用户的部门
                Person person = _repository.GetPersonByUser(user.Id);
                if (person != null)
                {
                    deptID = person.DepartmentId;
                }
            }
            SearchPageVO<Person> result = new SearchPageVO<Person>();
            int pageCount = 0;
            var list = _repository.GetPersons(deptID, search, hasUser, pageSize, pageCurrent, out pageCount);
            result.PageCurrent = pageCurrent;
            result.PageSize = pageSize;
            result.DataList = list;
            result.RowCount = pageCount;

            return result;
        }



    }
}
