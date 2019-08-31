using System;
using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 人员考核成绩
    /// </summary>
    public class PersonAssessmentGrade : EntityBase
    {
        protected PersonAssessmentGrade() { }
        public PersonAssessmentGrade(Assessment assessment, Person person, bool isAbsent,
            bool isMakeup, IEnumerable<SubjectGrade> subjectGrades)
        {
            this.Assessment = assessment;
            this.Person = person;
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.SubjectGrades = subjectGrades;
        }
        public string AssessmentId { get; protected set; }
        public Assessment Assessment { get; protected set; }
        public string PersonId { get; protected set; }
        public Person Person { get; protected set; }

        /// <summary>
        /// 是否缺考
        /// </summary>
        public bool IsAbsent { get; protected set; }
        /// <summary>
        /// 是否补考
        /// </summary>
        public bool IsMakeup { get; protected set; }
        public IEnumerable<SubjectGrade> SubjectGrades { get; protected set; }

        public void Update(Assessment assessment, Person person, bool isAbsent,
            bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            this.Assessment = assessment;
            this.Person = person;
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.SubjectGrades = grades;
        }

        public void CalculateScore()
        {
            foreach (SubjectGrade subjectGrade in SubjectGrades)
            {
                
                SubjectConversion subjectConversion;
                try
                {
                      subjectConversion = subjectGrade.Subject.GetSubjectConversion(Person.Sex);
                }
                catch (Exception ex)
                {
                    throw new Exceptions.ConversionError(subjectGrade.Subject.Name, Person.RealName, Person.Sex, ex.Message);
                }
                try { 
                    var score=subjectConversion.ConversionTable.CalculateScore
                        (subjectGrade.Subject.IsQualifiedConversion,Person.Age,subjectGrade.Grade.Value);
                    
                      subjectGrade.SetScore(score);  
                    }
                catch(Exception ex)
                {
                    throw new Exceptions.CalculateScoreError(subjectGrade.Subject.Name, Person.RealName,  ex.Message);
                }
                

            }

        }

    }


}
