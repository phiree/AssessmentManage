using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class AssessmentService : IAssessmentService
    {

       
        IGradeCalculater gradeCalculater;
        public AssessmentService(IGradeCalculater gradeCalculater)
        {
            this.gradeCalculater = gradeCalculater;
        }
        /// <summary>
        /// 保存用户得分.
        /// </summary>
        /// <param name="assessment"></param>
        /// <param name="person"></param>
        /// <param name="grades"></param>
        public void SavePersonGrade(Assessment assessment, Person person, bool isAbsent, bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            foreach (var subjectGrade in grades)
            {
                var subject = subjectGrade.Subject;
                //if (!assessment.Contains(subject.Id))
                //{
                //    throw new Exceptions.AssessmentNotContainSubject(assessment.Name, subject.Name);
                //}
                gradeCalculater.CalculateGrade(subjectGrade, grades);
            }


            PersonGrade personGrade = new PersonGrade(assessment.Id, person.Id, isAbsent, isMakeup, grades);
            
        }
       
    }
}
