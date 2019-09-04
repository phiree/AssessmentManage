using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface IPersonApplication : IApplicationBase<Person>
    {
        PersonSearchVO GetPersons(string name, string idNo, int pageSize, int pageCurrent);
        bool DeletePersons(string personId);

    }
}
