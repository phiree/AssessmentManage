using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models
{
    public class DepartmentVO
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 父级部门
        /// </summary>
        [JsonProperty("parentId")]
        public string ParentId { get; set; }
    }

    public class DepartmentChangeVO : DepartmentVO
    {
        /// <summary>
        /// 部门ID；修改时使用
        /// </summary>
        [Required]
        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
