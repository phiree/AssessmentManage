using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Api.Models;
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
        public Subject Create([FromBody]SubjectVO model)
        {
            if (model.ParamSubjects != null && model.ParamSubjects.Count != 0)
            {
                IList<ParamSubject> paramSubjects = new List<ParamSubject>();
                foreach (var ps in model.ParamSubjects)
                {
                    paramSubjects.Add(new ParamSubject(ps.SortOrder, ps.SubjectId, ps.SubjectName));
                }
                return subjectApplication.CreateComputedSubject(model.Name, model.SubjectType, model.SexLimitation, model.IsQualifiedConversion, model.Unit, model.Formula, paramSubjects);
            }
            else
            {
                return subjectApplication.Create(model.Name, model.SubjectType, model.SexLimitation, model.IsQualifiedConversion, model.Unit);
            }
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
        /// <param name="paramSubjects">参与计算的科目</param>
        /// <returns></returns>
        //[HttpPost("createcomputed")]
        //public Subject CreateComputedSubject(string name, SubjectType subjectType,
        //    SexLimitation sexLimitation, bool isQualifiedConversion, string unit,
        //    IList<ParamSubject> paramSubjects, string formula)
        //{
        //    var subject = subjectApplication.CreateComputedSubject(name, subjectType, sexLimitation, isQualifiedConversion, unit, formula, paramSubjects);
        //    return subject;
        //}
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
        public Subject Update([FromBody]SubjectChangeVO model)
        {
            if (model.ParamSubjects != null && model.ParamSubjects.Count != 0)
            {
                IList<ParamSubject> paramSubjects = new List<ParamSubject>();
                foreach (var ps in model.ParamSubjects)
                {
                    paramSubjects.Add(new ParamSubject(ps.SortOrder, ps.SubjectId, ps.SubjectName));
                }

                ComputedSubject subject = (ComputedSubject)subjectApplication.Get(model.SubjectId);
                subject.UpdateComputedSubject(model.Name, model.SubjectType,
                  model.SexLimitation, model.IsQualifiedConversion, model.Unit, model.Formula, paramSubjects);
                subjectApplication.Update(subject);
                return subject;
            }
            else
            {
                Subject subject = subjectApplication.Get(model.SubjectId);
                subject.Update(model.Name, model.SubjectType,
                  model.SexLimitation, model.IsQualifiedConversion, model.Unit);
                subjectApplication.Update(subject);
                return subject;
            }


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
        //[HttpPost("udpatecomputed")]
        //public Subject UpdateComputedSubject(string subjectId, string name, SubjectType subjectType,
        //    SexLimitation sexLimitation, bool isQualifiedConversion, string unit,
        //    IDictionary<int, string> paramSubjects, string formula)
        //{
        //    throw new NotImplementedException();
        //}


        /// <summary>
        /// 初始化成绩换算表
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="floorAge">年龄范围</param>
        /// <param name="cellingAge">年龄范围</param>
        /// <param name="score">分数</param>
        /// <param name="sex">性别,如果科目类型为4(不限性别,但是换算规则不同BothButWithDifirentConversion)则需要赋值,否则传空</param>
        /// <returns></returns>
        [HttpGet("InitConversionTable")]
        public ConversionTable InitConversionTable(string subjectId, Sex sex, int floorAge, int cellingAge, int score)
        {
            return subjectApplication.InitConversion(subjectId, sex, new AgeRange(floorAge, cellingAge), score);
        }
        /// <summary>
        /// 获取成绩换算表
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        [HttpGet("GetConversionTable")]
        public ConversionTable GetConversionTable(string subjectId, Sex sex)
        {
            return subjectApplication.GetConversionTable(subjectId, sex);
        }


        /// <summary>
        /// 换算表添加得分
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        [HttpGet("AddScore")]
        public ConversionTable AddScore(string subjectId, Sex sex, double score)
        {
            return subjectApplication.AddScore(subjectId, sex, score);
        }

        /// <summary>
        /// 添加年龄范围
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="floorAge"></param>
        /// <param name="cellingAge"></param>
        [HttpGet("AddAgeRange")]
        public ConversionTable AddAgeRange(string subjectId, Sex sex, int floorAge, int cellingAge)
        {
            return subjectApplication.AddAgeRange(subjectId, sex, new AgeRange(floorAge, cellingAge));
        }
        /// <summary>
        /// 移除得分
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="score"></param>
        [HttpGet("RemoveScore")]
        public ConversionTable RemoveScore(string subjectId, Sex sex, double score)
        {
            return subjectApplication.RemoveScore(subjectId, sex, score);
        }
        /// <summary>
        /// 移除年龄范围
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="floorAge"></param>
        /// <param name="cellingAge"></param>
        [HttpGet("RemoveAgeRange")]
        public ConversionTable RemoveAgeRange(string subjectId, Sex sex, int floorAge, int cellingAge)
        {
            return subjectApplication.RemoveAgeRange(subjectId, sex, new AgeRange(floorAge, cellingAge));
        }

        /// <summary>
        /// 设置对应的成绩
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="sex"></param>
        /// <param name="floorAge"></param>
        /// <param name="cellingAge"></param>
        /// <param name="score"></param>
        /// <param name="grade"></param>
        [HttpGet("SetGrade")]
        public ConversionTable SetGrade(string subjectId, Sex sex, int floorAge, int cellingAge, double score, double grade)
        {
            return subjectApplication.SetGrade(subjectId, sex, new AgeRange(floorAge, cellingAge), score, grade);
        }
    }
}