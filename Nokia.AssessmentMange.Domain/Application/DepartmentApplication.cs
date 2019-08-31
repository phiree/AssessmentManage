using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class DepartmentApplication : ApplicationBase<Department>, IDepartmentApplication
    {
        IDepartmentRepository  departmentRepository;
        public DepartmentApplication(IDepartmentRepository departmentRepository) 
            :base(departmentRepository)
        { 
            this.departmentRepository=departmentRepository;
            }

        public Department Create(string name, Department parent)
        {
            throw new NotImplementedException();
        }
        public Department GetWithAllChildren(string id)
        {
           var department= departmentRepository.GetWithAllChildren(id);
            return department;
        }

    }
}
