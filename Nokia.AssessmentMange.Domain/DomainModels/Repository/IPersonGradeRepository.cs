using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IPersonGradeRepository : IEFCRepository<PersonAssessmentGrade>
    {
        /// <summary>
        /// 人员的考核成绩
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        IEnumerable<PersonAssessmentGrade> FindByAssessmentAndPerson(string assessmentId, string personId);

        PersonAssessmentGrade GetByPersonAssessment(string personId, string assessmentId);

        IEnumerable<PersonAssessmentGrade> GetByPersonAssessment(string assessmentId);
        int GetCountByAssessment(string assessmentId);
    }
}
