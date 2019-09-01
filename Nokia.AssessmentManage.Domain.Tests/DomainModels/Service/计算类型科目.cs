using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using Nokia.AssessmentMange.Domain.Infrastructure;
using Nokia.AssessmentMange.Domain.DomainModels;
using System.Diagnostics;

namespace Nokia.AssessmentManage.Domain.Tests.DomainModels.Service
{
    public class AssessmentServiceTests
    {
        [Fact]
        public void 计算类型科目的成绩计算Test()
        {
            
            var subjectHeight = new Subject("身高", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "CM");
            var subjectWeight = new Subject("体重", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "KG");
            var subject100Race = new Subject("百米跑", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "秒");
            var subjectBMI = new ComputedSubject("BMI", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "CM",
                                new List<ParamSubject> { new ParamSubject(1, subjectHeight ), new ParamSubject ( 2, subjectWeight ) }, "$1/$2");
            var subjectGrades= new List<SubjectGrade>
                   {
                        new SubjectGrade(subjectHeight,118),
                        new SubjectGrade(subjectWeight,89),
                        new SubjectGrade(subject100Race,12),
                        new SubjectGrade(subjectBMI,null),
                   };
            GradeCalculater gc=new GradeCalculater(new CodeRunner());
            gc.CalculateGrade(
                subjectGrades[3],
                   subjectGrades
                    
                       );
            foreach(var sg in subjectGrades)
            { 
                Debug.WriteLine($"{sg.Subject.Name}:{sg.Grade}");
                }
     
            Assert.Equal((118m/89m).ToString("{0:2}"),subjectGrades[3].Grade.Value.ToString("{0:2}"));

        }
    }
}
