using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Repository
{
    public interface IPersonGradeRepository:IEFCRepository<PersonGrade>
    {
        /// <summary>
        /// 人员的考核成绩
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        IEnumerable<PersonGrade> FindByAssessmentAndPerson(string assessmentId,string personId);
    }
}
