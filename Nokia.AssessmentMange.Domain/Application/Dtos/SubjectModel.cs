using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    public class SubjectModel
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        /// <summary>
        /// 分类
        /// </summary>
        public SubjectType SubjectType { get; protected set; }
        public SexLimitation SexLimitation { get; protected set; }
        /// <summary>
        /// 得分换算表是否使用 合格/不合格  
        /// </summary>
       
        public bool IsQualifiedConversion { get; protected set; }
        public string Unit { get; protected set; }
    }
    public class ComputedSubjectModel:SubjectModel
    {
        public IList<ParamSubjectModel> ParamSubjects { get;   set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get;   set; }
    }
    public class ParamSubjectModel
    {
        public int SortOrder { get; set; }
        public string PSubjectId { get; set; }
        public string PSubjectName { get; set; }
    }
}
