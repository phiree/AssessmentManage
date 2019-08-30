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
            bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            this.Assessment  = assessment ;
            this.Person  = person ;
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.Grades = grades;
        }
        public string AssessmentId { get; protected set; }
        public Assessment Assessment { get;protected set;}
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
        public IEnumerable<SubjectGrade> Grades { get; protected set; }

        public void Update(Assessment assessment, Person person, bool isAbsent,
            bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            this.Assessment = assessment;
            this.Person = person;
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.Grades = grades;
        }
        
                  
    }


}
