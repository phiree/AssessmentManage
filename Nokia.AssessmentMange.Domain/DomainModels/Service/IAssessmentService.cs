using Nokia.AssessmentMange.Domain.Application.Dtos;
using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public interface IAssessmentService
    {
        void SavePersonGrade(Assessment assessment, Person person, bool isAbsent, bool isMakeup, IEnumerable<SubjectGrade> subjectGrades);
        SearchPageVO<Assessment> GetList(string departmentID, int pageSize, int pageCurrent);
    }
}