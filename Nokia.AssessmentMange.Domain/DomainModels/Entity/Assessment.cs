using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Assessment
    {
        /// <summary>
        /// 对应的部门
        /// </summary>
        public string DepartmentId { get;protected set;}
        /// <summary>
        /// 考核名称
        /// </summary>
        public string Name { get;protected set;}
        /// <summary>
        /// 包含的科目
        /// </summary>
        public IList<Subject> Subjects { get;protected set;}

        /// <summary>
        /// 是否是年度考核
        /// </summary>
        public bool Annual { get;protected set;}
        public DateTime CreatedTime { get;protected  set;}
    }
}
