using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IDepartmentRepository : IEFCRepository<Department>
    {
        List<Department> GetWithAllChildren();

        Department GetWithAllChildren(string id);
    }
}
