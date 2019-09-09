using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IAssessmentRepository
    {
        List<Assessment> GetList(string departmentID, int pageSize, int pageCurrent, out int rowCount);
        Assessment GetAssessment(string assessmentID);

    }
}
