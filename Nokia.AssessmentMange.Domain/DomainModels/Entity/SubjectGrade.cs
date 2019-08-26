using System;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目成绩
    /// </summary>
    public class SubjectGrade
    {
        public Subject Subject { get;protected set;}
        public double? Grade { get; protected set; }

        
    }


}
