using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Nokia.AssessmentManage.Domain.Tests.DomainModels.Service
{
   public class 提交人员成绩
    {
        [Fact]
        public void 提交成绩_第一次提交()
        {
            Subject subject=new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false,"秒");
          var  personAssessmentGrade =new PersonAssessmentGrade(
                 new Assessment("dd","name",false,new List<Subject>{ subject}),
                 new Person("realname",DateTime.Now,Sex.Female,"") 
                 
                 );
            var firstCommit=new AssessmentGrade(true,true,new List<SubjectGrade>{ new SubjectGrade(subject,14) });
            //第一次提交的是补考,报错
            Assert.Throws < FirstAssessmentGradeShouldNotBeMakeup >(()=>{
                personAssessmentGrade.CommitGrade(firstCommit);
                });
            //第一次提交的不是补考,直接附加
            firstCommit = new AssessmentGrade(true, false, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            personAssessmentGrade.CommitGrade(firstCommit);
            Assert.Equal(14,personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);


        }
        [Fact]
        public void 提交成绩_第二次提交()
        {
            Subject subject = new Subject("百米", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒");
            var firstCommit = new AssessmentGrade(true, false, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            var personAssessmentGrade = new PersonAssessmentGrade(
                   new Assessment("dd", "name", false, new List<Subject> { subject }),
                   new Person("realname", DateTime.Now, Sex.Female, "") 
                  
                   );
            //第二次提交的不是补考,直接修改
            var secondCommit = new AssessmentGrade(false, false, new List<SubjectGrade> { new SubjectGrade(subject, 12) });
            personAssessmentGrade.CommitGrade(secondCommit);
            Assert.Single(personAssessmentGrade.AssessmentGrades);
            Assert.Equal(12, personAssessmentGrade.AssessmentGrades.First().SubjectGrades.First().Grade);

            //第二次提交的是补考,附加
            secondCommit = new AssessmentGrade(true, true, new List<SubjectGrade> { new SubjectGrade(subject, 14) });
            personAssessmentGrade.CommitGrade(secondCommit);
            Assert.Equal(2, personAssessmentGrade.AssessmentGrades.Count());
            Assert.Equal(14, personAssessmentGrade.AssessmentGrades.ToList()[1].SubjectGrades.First().Grade);


        }


    }
}
