using System.Collections.Generic;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 人员考核成绩
    /// </summary>
    public class PersonGrade : Entity.EntityBase
    {
        protected PersonGrade() { }
        public PersonGrade(string assessmentId, string personId, bool isAbsent, bool isMakeup, IEnumerable<SubjectGrade> grades)
        {
            this.AssessmentId = assessmentId;
            this.PersonId = personId;
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.Grades = grades;
        }
        public string AssessmentId { get; protected set; }
        public string PersonId { get; protected set; }

        /// <summary>
        /// 是否缺考
        /// </summary>
        public bool IsAbsent { get; protected set; }
        /// <summary>
        /// 是否补考
        /// </summary>
        public bool IsMakeup { get; protected set; }
        public IEnumerable<SubjectGrade> Grades { get; protected set; }


    }


}
