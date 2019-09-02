using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 考核 项目 关联关系
    /// </summary>
   public class AssessmentSubject
    {
        protected AssessmentSubject() { }
        public AssessmentSubject(Assessment Assessment, Subject Subject) { 
            this.Assessment=Assessment;
            this.Subject=Subject;
            this.AssessmentId=Assessment.Id;
            this.SubjectId=Subject.Id;
            }

        
        public string AssessmentId { get; set; }
        public Assessment Assessment { get;set;}

        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
