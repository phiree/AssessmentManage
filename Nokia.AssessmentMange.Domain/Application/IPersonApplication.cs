using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IPersonApplication : IApplicationBase<Person>
    {
        SearchPageVO<Person> GetPersons(User user, string deptID, string name, string idNo, int pageSize, int pageCurrent);
        SearchPageVO<Person> GetPersons(User user, string deptID, string search, int? hasUser, int pageSize, int pageCurrent);
        bool DeletePersons(string personId);

    }
}
