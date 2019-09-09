using Microsoft.EntityFrameworkCore;
using Nokia.AssessmentMange.Domain.Common;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class DepartmentRepository : EFCRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(AssessmentDbContext conn) : base(conn) { }
        public Department GetWithSingleChildren(string id, bool isAdmin)
        {
            //var department = Conn.Departments.Include(c => c.Children).AsEnumerable().FirstOrDefault(x => x.Id == id);
            //return department;

            var department = Conn.Departments.AsEnumerable().FirstOrDefault(x => x.Id == id && x.State == 1);
            if (department == null) { return null; }
            return RecursionChild(department.Id, GetAllDepar(isAdmin));
        }

        public List<Department> GetWithAllChildren(bool isAdmin)
        {
            var where = PredicateBuilder.True<Department>();
            where = where.And(x => x.ParentId == null && x.State == 1);

            var result = Conn.Departments.Where(where);
            if (result == null) { return null; }
            List<Department> list = new List<Department>();
            foreach (var depart in result)
            {
                list.Add(RecursionChild(depart.Id, GetAllDepar(isAdmin)));
            }
            return list;
        }

        public List<Department> GetWithAllChildren(string id, bool isAdmin)
        {
            var result = Conn.Departments.Where(x => x.Id == id && x.State == 1);
            if (result == null) { return null; }
            List<Department> list = new List<Department>();
            foreach (var depart in result)
            {
                list.Add(RecursionChild(depart.Id, GetAllDepar(isAdmin)));
            }
            return list;
            //return Conn.Departments.Include(c => c.Children).Where(x => x.Id == id).ToList();
        }


        private IEnumerable<Department> GetAllDepar(bool isAdmin)
        {
            var where = PredicateBuilder.True<Department>();
            where = where.And(x => x.State == 1);
            return Conn.Departments.Where(where);
        }

        private Department RecursionChild(string id, IEnumerable<Department> deptList)
        {
            Department dept = deptList.FirstOrDefault(item => item.Id == id);
            if (dept == null) { return null; }
            var childList = deptList.Where(item => item.ParentId == dept.Id);

            if (childList != null && childList.Count() != 0)
            {
                if (dept.Children == null) { dept.Children = new List<Department>(); }
                foreach (var d in childList)
                {
                    dept.Children.Add(RecursionChild(d.Id, deptList));
                }
            }
            return dept;
        }

        public int GetChildrenByID(string id)
        {
            return Conn.Departments.Count(item => item.ParentId == id);
        }
    }
}
