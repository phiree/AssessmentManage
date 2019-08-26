using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class AssessmentService
    {

       
        IGradeCalculater gradeCalculater;
        public AssessmentService(IGradeCalculater gradeCalculater)
        {
            this.gradeCalculater = gradeCalculater;
        }
        /// <summary>
        /// 保存人员成绩
        /// </summary>
        /// <param name="assessment"></param>
        /// <param name="person"></param>
        /// <param name="subjectGrades"></param>
        public void SavePersonGrade(Assessment assessment, Person person, bool isAbsent, bool isMakeup, IList<SubjectGrade> subjectGrades)
        {
            foreach (var subjectGrade in subjectGrades)
            {
                var subject = subjectGrade.Subject;
                if (!assessment.Contains(subject.Id))
                {
                    throw new Exceptions.AssessmentNotContainSubject(assessment.Name, subject.Name);
                }
                gradeCalculater.CalculateGrade(subjectGrade, subjectGrades);
            }

            PersonGrade personGrade = new PersonGrade(assessment.Id, person.Id, isAbsent, isMakeup, subjectGrades);

        }
       
    }
}
