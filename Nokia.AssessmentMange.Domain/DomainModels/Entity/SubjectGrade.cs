using System;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目成绩
    /// </summary>
    public class SubjectGrade:Entity.EntityBase
    {
        protected SubjectGrade() { }
        public SubjectGrade(Subject subject,double? grade)
        { 
            this.Subject=subject;
            this.Grade=grade;
            }
        
        public Subject Subject { get;protected set;}
        public double? Grade { get;   set; }

        
    }


}
