using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Assessment : Entity.EntityBase
    {
        protected Assessment() { }
        public Assessment(string departmentId, string name,   bool annual)
        {
            this.DepartmentId = departmentId;
           // this.Subjects = subjects;
            this.Annual = annual;
            this.CreatedTime = DateTime.Now;

        }
        /// <summary>
        /// 对应的部门
        /// </summary>
        public string DepartmentId { get; protected set; }
        /// <summary>
        /// 考核名称
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// 包含的科目
        /// </summary>


        /// <summary>
        /// 是否是年度考核
        /// </summary>
        [Column("Annual", TypeName = "bit")]
        public bool Annual { get; protected set; }
        public DateTime CreatedTime { get; protected set; }

        
    }
}
