using Newtonsoft.Json;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models
{
    public class PersonAssessmentGradeVO
    {
        /// <summary>
        /// 人员考核成绩Id
        /// </summary>
        [Required]
        [JsonProperty("personAssessmentId")]
        public string PersonAssessmentId { get; set; }
        /// <summary>
        /// 是否缺考
        /// </summary>
        [JsonProperty("isAbsent")]
        public bool IsAbsent { get; set; }
        /// <summary>
        /// 是否补考
        /// </summary>
        [JsonProperty("isMakeup")]
        public bool IsMakeup { get; set; }
        /// <summary>
        /// 各科目的成绩
        /// </summary>
        [JsonProperty("subjectGrades")]
        public IList<SubjectGradeModel> SubjectGrades { get; set; }
    }
}
