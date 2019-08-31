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
       PersonAssessmentGradeApplication personAssessmentGradeApplication;
        public PersonAssessmentGradeController(PersonAssessmentGradeApplication personAssessmentGradeApplication)
        {
            this.personAssessmentGradeApplication= personAssessmentGradeApplication;

        }
        /// <summary>
        /// 获取人员考核成绩
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="assessmentId"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public PersonAssessmentGrade Get(string personId,string assessmentId)
        { 
            throw new NotImplementedException();
            }
        /// <summary>
        /// 录入人员成绩
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost("EntryGrade")]
        public PersonAssessmentGrade EntryGrade(PersonAssessementGradeCreateModel createModel)
        {
          return  personAssessmentGradeApplication.Create(createModel);

        }
        [HttpPost("update")]
        public PersonAssessmentGrade Update(PersonAssessementGradeUpdateModel updateModel)
        {
            return personAssessmentGradeApplication.Create(updateModel);

           

        }
    }
}