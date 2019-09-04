using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IDepartmentApplication : IApplicationBase<Department>
    {
        // Department Create(string name,Department parent);
        List<Department> GetWithAllChildren();
        Department GetWithAllChildren(string id);
    }
}
