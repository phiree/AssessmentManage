using Newtonsoft.Json;
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
        [JsonProperty("departmentId")]
        public string DepartmentId { get; protected set; }
        /// <summary>
        /// 考核名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; protected set; }

        /// <summary>
        /// 是否是年度考核
        /// <summary>
        [JsonProperty("annual")]
        public bool Annual { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 包含的科目
        /// </summary>
        [JsonProperty("subjects")]
        public IList<Subject> Subjects { get; set; }
    }
    public class AssessmentCreateModel : AssessmentModel
    {

    }
    public class AssessmentUpdateModel : AssessmentModel
    {
        [Required]
        public string Id { get; set; }

    }
}
