using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    public class AssessmentModel
    {
        /// <summary>
        /// 对应的部门
        /// </summary>
        public string DepartmentId { get; protected set; }
        /// <summary>
        /// 考核名称
        /// </summary>
        public string Name { get; protected set; }


        /// <summary>
        /// 是否是年度考核
        /// <summary>
        public bool Annual { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; protected set; }
        /// <summary>
        /// 包含的科目
        /// </summary>
        public IList<Subject> Subjects { get; set; }
    }
    public class AssessmentCreateModel:AssessmentModel
    {
        
    }
    public class AssessmentUpdateModel
    {
        [Required]
        public string Id { get; set; }
       
    }
}
