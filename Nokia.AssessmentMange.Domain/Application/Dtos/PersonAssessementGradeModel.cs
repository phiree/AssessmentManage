using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    /// <summary>
    /// 人员考核成绩输入
    /// </summary>
    public class PersonAssessementGradeCreateModel
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
        public IEnumerable<SubjectGradeModel> Grades { get; protected set; }
    }
    public class SubjectGradeModel
    { 
        public string SubjectId { get;set;}
        public double? Grade { get;set;} 
        }

    public class PersonAssessementGradeUpdateModel: PersonAssessementGradeCreateModel
    {
       public string Id { get;set;}
    }
}
