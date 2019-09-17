using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure;
using Nokia.AssessmentMange.Domain.Infrastructure;

namespace Nokia.AssessmentManage.Domain.Tests.DomainModels.Service
{
   public class 提交人员成绩
    {
        IGradeCalculater gradeCalculater = new GradeCalculater(new CodeRunner());
        [Fact]
        public void 提交成绩_第一次提交()
        {
            Subject subject=new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false,"秒");
          var  personAssessmentGrade =new PersonAssessmentGrade(
                 new Assessment("dd","name",false,new List<Subject>{ subject}),
                 new Person(Guid.NewGuid().ToString(), "realname",DateTime.Now,Sex.Female) 
                 
                 );
            var firstCommit=new AssessmentGrade(true,true,new List<SubjectGrade>{ new SubjectGrade(subject,14) });
            //第一次提交的是补考,报错
            Assert.Throws < FirstAssessmentGradeShouldNotBeMakeup >(()=>{
                personAssessmentGrade.CommitGrade(firstCommit,gradeCalculater);
                });
            //第一次提交的不是补考,直接附加
            firstCommit = new AssessmentGrade(true, false, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            personAssessmentGrade.CommitGrade(firstCommit, gradeCalculater);
            Assert.Equal(14,personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);


        }
        [Fact]
        public void 提交成绩_第二次提交()
        {
            Subject subject = new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒");
            var firstCommit = new AssessmentGrade(true, false, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            var personAssessmentGrade = new PersonAssessmentGrade(
                   new Assessment("dd", "name", false, new List<Subject> { subject }),
                   new Person(Guid.NewGuid().ToString(), "realname", DateTime.Now, Sex.Female) 
                  
                   );
            //第二次提交的不是补考,直接修改
            var secondCommit = new AssessmentGrade(false, false, new List<SubjectGrade> { new SubjectGrade(subject, 12) });
            personAssessmentGrade.CommitGrade(secondCommit, gradeCalculater);
            Assert.Single(personAssessmentGrade.AssessmentGrades);
            Assert.Equal(12, personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);

            //第二次提交的是补考,附加
            secondCommit = new AssessmentGrade(true, true, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            personAssessmentGrade.CommitGrade(secondCommit, gradeCalculater);
            Assert.Equal(2, personAssessmentGrade.AssessmentGrades.Count());
            Assert.Equal(14, personAssessmentGrade.AssessmentGrades.ToList()[1].SubjectGrades.First().Grade);


        }

        [Fact]
        public void 提交计算类型科目的成绩()
        {
            Subject subject = new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒");
            var subjectHeight = new Subject("身高", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "CM");
            var subjectWeight = new Subject("体重", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "KG");
            var subject100Race = new Subject("百米跑", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "秒");
            var subjectBMI = new ComputedSubject("BMI", SubjectType.Intelligent, SexLimitation.BothAndSameConversion, false, "CM",
                                new List<ParamSubject> { new ParamSubject(1, subjectHeight), new ParamSubject(2, subjectWeight) }, "$1/$2");


            var firstCommit = new AssessmentGrade(true, false, new List<SubjectGrade>
                   {
                        new SubjectGrade(subjectHeight,118),
                        new SubjectGrade(subjectWeight,89),
                        new SubjectGrade(subject100Race,12),
                        new SubjectGrade(subjectBMI,null),
                   });
            var personAssessmentGrade = new PersonAssessmentGrade(
                   new Assessment("dd", "name", false, new List<Subject> { subject,subjectHeight,subjectWeight,subject100Race,subjectBMI }),
                   new Person(Guid.NewGuid().ToString(), "realname", DateTime.Now, Sex.Female)

                   );
            personAssessmentGrade.CommitGrade(firstCommit, gradeCalculater);

            Assert.Equal((118m / 89m).ToString("{0:2}"), personAssessmentGrade.AssessmentGrades[0].SubjectGrades.ToList()[3].Grade.Value.ToString("{0:2}"));
            

        }
    }
}
