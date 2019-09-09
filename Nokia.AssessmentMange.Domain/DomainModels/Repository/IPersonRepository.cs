using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IPersonRepository
    {
        int GetPersonCountByDepartment(string deptID);
        Person GetPersonByUser(string uid);

        List<Person> GetPersons(string deptID, string search, int? hasUser, int pageSize, int pageCurrent, out int rowCount);
    }
}
