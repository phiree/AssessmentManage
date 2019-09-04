using Newtonsoft.Json;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models
{
    /// <summary>
    /// 科目
    /// </summary>
    public class SubjectVO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get;  set; }
        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty("subjectType")]
        public SubjectType SubjectType { get;  set; }
        [JsonProperty("sexLimitation")]
        public SexLimitation SexLimitation { get;  set; }
        /// <summary>
        /// 得分换算表是否使用 合格/不合格  
        /// </summary>
        [JsonProperty("isQualifiedConversion")]
        public bool IsQualifiedConversion { get;  set; }
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty("unit")]
        public string Unit { get;  set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        [JsonProperty("formula")]
        public string Formula { get; set; }
        /// <summary>
        /// 计算科目
        /// </summary>
        [JsonProperty("paramSubjects")]
        public List<ParamSubjectVO> ParamSubjects { get; set; }
    }

    public class SubjectChangeVO : SubjectVO
    {
        /// <summary>
        /// 科目计算Id
        /// </summary>
        [Required]
        [JsonProperty("subjectId")]
        public string SubjectId { get; set; }
    }

    /// <summary>
    /// 计算科目
    /// </summary>
    public class ParamSubjectVO
    {
        /// <summary>
        /// 科目ID
        /// </summary>
        [Required]
        [JsonProperty("subjectId")]
        public string SubjectId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
    }
}
