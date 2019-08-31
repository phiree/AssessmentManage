using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class DepartmentApplication : ApplicationBase<Department>, IDepartmentApplication
    {
        IRepository<Department> departmentRepository;
        public DepartmentApplication(IRepository<Department> departmentRepository) 
            :base(departmentRepository)
        { }
        
    }
}
