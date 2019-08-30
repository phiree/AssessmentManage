using System;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目成绩
    /// </summary>
    public class SubjectGrade
    {
        protected SubjectGrade() { }
        public SubjectGrade(Subject subject,double? grade)
        { 
            this.Subject=subject;
            this.Grade=grade;
            }
        public string SubjectId { get;protected set;}
        public Subject Subject { get;protected set;}
        public double? Grade { get;   set; }

        
    }


}
