using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Application.Dtos;

using Nokia.AssessmentMange.Domain.Application;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 人员考核成绩管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonAssessmentGradeController : ControllerBase
    {
        IPersonAssessmentGradeApplication personAssessmentGradeApplication;
        public PersonAssessmentGradeController(IPersonAssessmentGradeApplication personAssessmentGradeApplication)
        {
            this.personAssessmentGradeApplication = personAssessmentGradeApplication;

        }
        /// <summary>
        /// 获取人员考核成绩
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="assessmentId"></param>
        /// <returns>人员考核成绩. 提交成绩时,需要传入此对象的Id</returns>
        [HttpGet("Get")]
        public PersonAssessmentGrade Get(string personId, string assessmentId)
        {
            return personAssessmentGradeApplication.Get(assessmentId, personId);
        }
        /// <summary>
        /// 录入成绩
        /// </summary>
        /// <param name="personAssessmentId">人员考核成绩Id</param>
        /// <param name="isAbsent">是否缺考</param>
        /// <param name="isMakeup">是否补考</param>
        /// <param name="subjectGrades">各科目的成绩</param>
        /// <returns></returns>
        [HttpPost("CommitGrade")]
        public PersonAssessmentGrade CommitGrade(string personAssessmentId, bool isAbsent, bool isMakeup, IList<SubjectGradeModel> subjectGrades)
        {
            return personAssessmentGradeApplication.CommitGrades(personAssessmentId, isAbsent, isMakeup, subjectGrades);

        }

    }
}