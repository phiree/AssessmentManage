using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.DomainModels;
namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 科目管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
     
    public class SubjectController : ControllerBase
    {
        ISubjectApplication subjectApplication;
        public SubjectController(ISubjectApplication subjectApplication)
        {
            this.subjectApplication = subjectApplication;
        }
        /// <summary>
        /// 创建科目
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="subjectType">类型</param>
        /// <param name="sexLimitation">性别限制</param>
        /// <param name="isQualifiedConversion">是否合格_不合格类型</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        [HttpPost("create")]
        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            return subjectApplication.Create(name, subjectType, sexLimitation, isQualifiedConversion, unit);

        }
        /// <summary>
        /// 创建计算类型科目
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="subjectType">类型</param>
        /// <param name="sexLimitation">性别限制</param>
        /// <param name="isQualifiedConversion">是否合格_不合格类型</param>
        /// <param name="unit">单位</param>
        /// <param name="formula">计算公式</param>
        /// <param name="paramSubjects">参与计算的科目,key是顺序,value是科目id</param>
        /// <returns></returns>
        [HttpPost("createcomputed")]        
        public Subject CreateComputedSubject(string name, SubjectType subjectType,
            SexLimitation sexLimitation, bool isQualifiedConversion, string unit,
            IDictionary<int,string> paramSubjects, string formula)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// 获取科目
        /// </summary>
        /// <param name="subjectId">科目Id</param>
        /// <returns></returns>
        [HttpGet("Get")]
        public Subject Get(string subjectId)
        {

            return subjectApplication.Get(subjectId);
        }
        /// <summary>
        /// 获取所有科目
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IEnumerable<Subject> GetAll()
        {
            return subjectApplication.GetAll();
        }
        /// <summary>
        /// 更新科目
        /// </summary>
        /// <param name="subjectId">科目Id</param>
        /// <param name="name">名称</param>
        /// <param name="subjectType">类型</param>
        /// <param name="sexLimitation">性别限制类型</param>
        /// <param name="isQualifiedConversion">是否合格_不合格类型</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        [HttpPost("udpate")]
        public Subject Update(string subjectId, string name, SubjectType subjectType,
            SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            var old = subjectApplication.Get(subjectId);
            old.Update(name, subjectType,
              sexLimitation, isQualifiedConversion, unit);


            subjectApplication.Update(old);
            return old;
        }
        /// <summary>
        /// 更新计算类型科目
        /// </summary>
        /// <param name="subjectId">科目Id</param>
        /// <param name="name">名称</param>
        /// <param name="subjectType">类型</param>
        /// <param name="sexLimitation">性别限制类型</param>
        /// <param name="isQualifiedConversion">是否合格_不合格类型</param>
        /// <param name="unit">单位</param>
        /// <param name="formula">计算公式</param>
        /// <param name="unit">参与计算的科目</param>
        /// <returns></returns>
        [HttpPost("udpatecomputed")]
        public Subject UpdateComputedSubject(string subjectId, string name, SubjectType subjectType,
            SexLimitation sexLimitation, bool isQualifiedConversion, string unit,
            IDictionary<int, string> paramSubjects, string formula)
        {
           throw new NotImplementedException();
        }

        
         
        /// <summary>
        /// 初始化成绩换算表
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="ageRange">年龄范围</param>
        /// <param name="score">分数</param>
        /// <param name="sex">性别,如果科目类型为4(不限性别,但是换算规则不同BothButWithDifirentConversion)则需要赋值,否则传空</param>
        /// <returns></returns>
        [HttpPost("InitConversionTable")]
        public void InitConversionTable(string subjectId,Sex sex, AgeRange ageRange,double score )
        { 
            throw new NotImplementedException();
            
            }
        /// <summary>
        /// 添加得分
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="score"></param>
        [HttpPost("AddScore")]
        public void AddScore(string subjectId,Sex sex,double score)
        { }
        /// <summary>
        /// 添加年龄范围
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="ageRange"></param>
        [HttpPost("AddAgeRange")]
        public void AddAgeRange(string subjectId, Sex sex, AgeRange ageRange)
        { }
        /// <summary>
        /// 移除得分
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="score"></param>
        [HttpPost("RemoveScore")]
        public void RemoveScore(string subjectId, Sex sex, double score)
        { }
        /// <summary>
        /// 移除年龄范围
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="ageRange"></param>
        [HttpPost("RemoveAgeRange")]
        public void RemoveAgeRange(string subjectId, Sex sex, AgeRange ageRange)
        { }

        /// <summary>
        /// 设置对应的成绩
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="ageRange"></param>
        /// <param name="score"></param>
        /// <param name="grade"></param>
        public void SetGrade(string subjectId,Sex sex,AgeRange ageRange,double score ,double grade)
        { }


    }
}