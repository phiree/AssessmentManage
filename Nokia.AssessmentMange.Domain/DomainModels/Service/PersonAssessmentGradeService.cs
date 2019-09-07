using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class PersonAssessmentGradeService
    {
        IGradeCalculater gradeCalculater;
        public PersonAssessmentGradeService(IGradeCalculater gradeCalculater)
        {
            this.gradeCalculater = gradeCalculater;
        }

        /// <summary>
        /// 提交人员成绩
        /// </summary>
        /// <param name="grades"></param>
        /// <returns></returns>
    }
}
