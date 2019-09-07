using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IDepartmentRepository : IEFCRepository<Department>
    {
        List<Department> GetWithAllChildren(bool isAdmin);
        List<Department> GetWithAllChildren(string id);

        Department GetWithSingleChildren(string id);
    }
}
