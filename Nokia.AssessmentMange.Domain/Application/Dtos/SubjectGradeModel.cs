using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    /// <summary>
    /// 人员考核成绩输入
    /// </summary>
    
    public class SubjectGradeModel
    { 
        public string SubjectId { get;set;}
        public double? Grade { get;set;} 
        }

 
}
