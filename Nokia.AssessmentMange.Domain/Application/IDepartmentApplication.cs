using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IDepartmentApplication:IApplicationBase<Department>
    {
        // Department Create(string name,Department parent);
          Department GetWithAllChildren(string id);
    }
}
