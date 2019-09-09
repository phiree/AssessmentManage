using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IDepartmentRepository : IEFCRepository<Department>
    {
        List<Department> GetWithAllChildren(bool isAdmin);
        List<Department> GetWithAllChildren(string id, bool isAdmin);

        Department GetWithSingleChildren(string id, bool isAdmin);
        int GetChildrenByID(string id);
    }
}
