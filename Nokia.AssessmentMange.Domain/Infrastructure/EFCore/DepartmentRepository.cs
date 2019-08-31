﻿using Microsoft.EntityFrameworkCore;
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
        
        public DepartmentRepository(AssessmentDbContext conn):base(conn) {}
        public Department GetWithAllChildren(string id)
        {
           var department=Conn .Departments.Include(c=>c.Children).AsEnumerable().First(x=>x.Id==id);
            return department;
        }
    }
}
