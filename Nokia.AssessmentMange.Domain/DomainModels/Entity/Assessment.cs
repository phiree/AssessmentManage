using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Assessment : EntityBase
    {
        protected Assessment() { }
        public Assessment(string departmentId, string name, bool annual, IList<Subject> subjects)
        {
            this.DepartmentId = departmentId;
            this.SubjectList = subjects == null ? null : subjects.Select(x => new AssessmentSubject(this, x)).ToArray();
            this.Annual = annual;
            this.Name = name;
            this.CreatedTime = DateTime.Now;
        }
        public Assessment(string id, string departmentId, string name, bool annual, IList<Subject> subjects) : this(departmentId, name, annual, subjects)
        {
            this.Id = id;
        }
        /// <summary>
        /// 对应的部门
        /// </summary>
        public string DepartmentId { get; protected set; }
        [NotMapped]
        public string DepartmentRootName { get; set; }

        /// <summary>
        /// 考核名称
        /// </summary>
        [MaxLength(100)]
        public string Name { get; protected set; }

        /// <summary>
        /// 是否是年度考核
        /// </summary>
        [Column("Annual", TypeName = "bit")]
        public bool Annual { get; protected set; }
        public DateTime CreatedTime { get; protected set; }
        /// <summary>
        /// 包含的科目
        /// </summary>
        public IList<AssessmentSubject> SubjectList { get; set; }


    }
}
