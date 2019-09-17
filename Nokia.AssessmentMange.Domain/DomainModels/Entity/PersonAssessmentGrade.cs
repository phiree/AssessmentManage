using System;
using System.Collections.Generic;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 人员考核成绩
    /// </summary>
    public class PersonAssessmentGrade : EntityBase
    {

        protected PersonAssessmentGrade()
        {
            AssessmentGrades = new List<AssessmentGrade>();
        }
        public PersonAssessmentGrade(string assessmentId, string personId

             ) : this()
        {

            this.PersonId = personId;
            this.AssessmentId = assessmentId;

        }
        public PersonAssessmentGrade(Assessment assessment, Person person

              ) : this()
        {

            this.Person = person;
            this.Assessment = assessment;

        }
        public string PersonId { get; protected set; }
        public Person Person { get; protected set; }
        public string AssessmentId { get; protected set; }
        public Assessment Assessment { get; protected set; }
        public IList<AssessmentGrade> AssessmentGrades { get; protected set; }

        //提交成绩
        public void CommitGrade(AssessmentGrade assessmentGrade, IGradeCalculater gradeCalculater)
        {
            bool isNew = false;

            //补考缺考的判断
            //一个人员最多有两次成绩,而且,一次是非补考,一次是补考.
            //如果第二次录入的不是补考成绩,则更新第一次成绩. 如果第二次录入的是补考成绩,而且已经有了补考成绩,则更新补考成绩

            //大于两次,异常
            if (AssessmentGrades.Count() >= 2)
            {
                throw new Exceptions.TooManyAssessmentGrade(Assessment.Name, Person.RealName);
            }
            //已有的成绩
            if (AssessmentGrades.Count() == 1)
            {
                //如果是补考,异常
                if (AssessmentGrades.First().IsMakeup)
                {
                    throw new Exceptions.FirstAssessmentGradeShouldNotBeMakeup(Assessment.Name, Person.RealName);

                }
                //如果不是补考
                else
                {
                    //录入补考
                    if (assessmentGrade.IsMakeup)
                    {
                        isNew = true;
                        //  AssessmentGrades.Append(assessmentGrade);
                    }
                    else
                    { //更新第一次成绩
                        isNew = false;
                        // AssessmentGrades.First().Update(assessmentGrade.IsAbsent, assessmentGrade.IsMakeup, assessmentGrade.SubjectGrades);
                    }
                }

            }
            //还没有录入成绩
            else
            {
                if (assessmentGrade.IsMakeup)
                {
                    throw new Exceptions.FirstAssessmentGradeShouldNotBeMakeup(Assessment.Name, Person.RealName);
                }
                isNew = true;
                // AssessmentGrades.Append(assessmentGrade);
            }

            //验证科目有效性.
            foreach (var grade in assessmentGrade.SubjectGrades)
            {

                //科目不属于考核
                if (Assessment.SubjectList.Where(x => x.SubjectId == grade.SubjectId).Count() == 0)
                {
                    throw new Exceptions.SubjectNotInAssessment(Assessment.Name, grade.Subject.Name);
                }

            }

            //计算成绩 
            foreach (var subjectGrade in assessmentGrade.SubjectGrades)
            {
                gradeCalculater.CalculateGrade(subjectGrade,assessmentGrade.SubjectGrades);
            }
            
            if (isNew) { this.AssessmentGrades.Add(assessmentGrade); }
            else
            {
                AssessmentGrades.First().Update(assessmentGrade.IsAbsent, assessmentGrade.IsMakeup, assessmentGrade.SubjectGrades);
            }
            



        }

    }



}
