using Newtonsoft.Json;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models
{
    public class PersonVO
    {

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public Sex Sex { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 军衔
        /// </summary>
        [JsonProperty("rank")]
        public MilitaryRank Rank { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        [JsonProperty("idNo")]
        public string IdNo { get; set; }
    }

    public class PersonChangeVO : PersonVO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
