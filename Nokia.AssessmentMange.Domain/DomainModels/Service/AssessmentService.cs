using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class AssessmentService
    {

        IInfrastructure.ICodeRunner codeRunner;
        public AssessmentService(IInfrastructure.ICodeRunner codeRunner)
        {
            this.codeRunner=codeRunner;
            }
        /// <summary>
        /// 保存人员成绩
        /// </summary>
        /// <param name="assessment"></param>
        /// <param name="person"></param>
        /// <param name="subjectGrades"></param>
        public void SavePersonGrade(Assessment assessment, Person person,bool isAbsent,bool isMakeup, IList<SubjectGrade> subjectGrades)
        {
            CalculateGrade(subjectGrades,assessment);
             PersonGrade personGrade=new PersonGrade(assessment.Id,person.Id,isAbsent,isMakeup, subjectGrades);

        }
        public void CalculateGrade(IList<SubjectGrade> subjectGrades, Assessment assessment)
        {
            foreach (var subjectGrade in subjectGrades)
            {
                var subject = subjectGrade.Subject;
                if (!assessment.Contains(subject.Id))
                {
                    throw new Exceptions.AssessmentNotContainSubject(assessment.Name, subject.Name);
                }
                if (subject is ComputedSubject)
                {
                    var paramSubjects = ((ComputedSubject)subject).ParamSubjects;
                    var formula = ((ComputedSubject)subject).Formula;
                    var parameterSubjectsGrade = subjectGrades.Where(x => paramSubjects.Select(y => y.Value.Id).Contains(x.Subject.Id));
                    var parameterSubjectsGradeWithIndex = paramSubjects.ToDictionary(prop => prop.Key, prop => parameterSubjectsGrade.First(x => x.Subject.Id == prop.Value.Id).Grade);
                    var formulaCode = new FormulaParser().Parse(formula, parameterSubjectsGradeWithIndex);
                    var formulaResult = Convert.ToDouble(codeRunner.RunCode(formulaCode));
                    subjectGrade.Grade = formulaResult;
                }
            }
            
        }
    }
}
