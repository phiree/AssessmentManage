using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application
{
    public interface  ISubjectApplication:IApplicationBase<Subject>
    {
         Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, 
             bool isQualifiedConversion, string unit);
          Subject CreateComputedSubject(string name, SubjectType subjectType,
          SexLimitation sexLimitation, bool isQualifiedConversion, string unit, string formula, IList<ParamSubject> paramSubjects);
          Subject GetWithParamSubject(string id);
        /// <summary>
        /// 初始化 成绩换算表
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="ageRange"></param>
        /// <param name="score"></param>
        ConversionTable InitConversion(string subjectId, Sex sex, AgeRange ageRange, double score);
        ConversionTable AddScore(string subjectId, Sex sex, double score);
    }
}
