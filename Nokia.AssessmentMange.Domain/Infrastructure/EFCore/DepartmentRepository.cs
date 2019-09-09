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
        public Department GetWithSingleChildren(string id)
        {
            var department = Conn.Departments.Include(c => c.Children).AsEnumerable().FirstOrDefault(x => x.Id == id);
            return department;
        }

        public List<Department> GetWithAllChildren(bool isAdmin)
        {
            var where = PredicateBuilder.True<Department>();
            where= where.And(x => x.ParentId == null);
            if (!isAdmin)
            {
                where= where.And(x => x.State == 1);
            }
            return Conn.Departments.Include(c => c.Children).Where(where).ToList();
        }

        public List<Department> GetWithAllChildren(string id)
        {
            return Conn.Departments.Include(c => c.Children).Where(x => x.Id == id).ToList();
        }
    }
}
