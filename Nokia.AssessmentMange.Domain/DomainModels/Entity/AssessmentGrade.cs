using System;
using System.Collections.Generic;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 人员考核成绩
    /// </summary>
    public class  AssessmentGrade : EntityBase
    {
        
        protected AssessmentGrade() { }
        public AssessmentGrade(   bool isAbsent,
            bool isMakeup, IEnumerable<SubjectGrade> subjectGrades)
        {
             
          
            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.SubjectGrades = subjectGrades;
        }
 
        /// <summary>
        /// 是否缺考
        /// </summary>
        public bool IsAbsent { get; protected set; }
        /// <summary>
        /// 是否补考
        /// </summary>
        public bool IsMakeup { get; protected set; }
        public IEnumerable<SubjectGrade> SubjectGrades { get; protected set; }

        public void Update(  bool isAbsent,
             bool isMakeup, IEnumerable<SubjectGrade> subjectGrades)
        {
            

            this.IsAbsent = isAbsent;
            this.IsMakeup = isMakeup;
            this.SubjectGrades = subjectGrades;
        }





    }


}
