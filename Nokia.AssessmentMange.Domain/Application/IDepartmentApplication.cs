using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IDepartmentApplication : IApplicationBase<Department>
    {
        // Department Create(string name,Department parent);
        List<Department> GetWithAllChildren(User user);
        List<Department> GetWithAllChildrenByUser(User user);

        Department GetWithSingleChildren(string id);

        bool DeleteDepartment(string id);
    }
}
