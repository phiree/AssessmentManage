using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 人员考核成绩概况
    /// </summary>
    public class PersonAssessment : Entity.EntityBase
    {
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
        public IList<SubjectGrade> Grades { get; protected set; }
    }
    /// <summary>
    /// 科目成绩
    /// </summary>
    public class SubjectGrade
    {
        public string SubjectId { get;protected set;}
        public double? Grade { get; protected set; }
    }


}
