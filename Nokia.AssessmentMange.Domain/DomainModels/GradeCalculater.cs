using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class GradeCalculater : IGradeCalculater
    {
        ICodeRunner codeRunner;
        public GradeCalculater(ICodeRunner codeRunner)
        { 
            this.codeRunner=codeRunner;
            }
        public void CalculateGrade(SubjectGrade subjectGrade, IEnumerable<SubjectGrade> subjectGrades)
        {
            var subject = subjectGrade.Subject;

            if (subject is ComputedSubject)
            {
                var paramSubjects = ((ComputedSubject)subject).ParamSubjects;
                var formula = ((ComputedSubject)subject).Formula;
                var parameterSubjectsGrade = subjectGrades.Where(x => paramSubjects.Select(y => y.PSubject.Id).Contains(x.Subject.Id));
                var parameterSubjectsGradeWithIndex = paramSubjects.ToDictionary(prop => prop.SortOrder, prop => parameterSubjectsGrade.First(x => x.Subject.Id == prop.PSubject.Id).Grade);
                var formulaCode = new FormulaParser().Parse(formula, parameterSubjectsGradeWithIndex);
                var formulaResult = Convert.ToDouble(codeRunner.RunCode(formulaCode));
                subjectGrade.Grade = formulaResult;

            }


        }
    }
}
