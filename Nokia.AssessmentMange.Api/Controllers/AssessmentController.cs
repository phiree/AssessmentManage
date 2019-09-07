using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Api.Controllers.Authentication;

namespace Nokia.AssessmentMange.Api.Controllers
{
    /// <summary>
    /// 考核管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : BaseController
    {
        IAssessmentApplication assessmentApplication;
        public AssessmentController(IAssessmentApplication assessmentApplication, IAuthenticateService authenticateService) : base(authenticateService)
        {
            this.assessmentApplication = assessmentApplication;

        }
        /// <summary>
        /// 创建一个考核
        /// </summary>
        /// <param name="assessmentModel">考核模型,id为空</param>
        /// <returns></returns>
        [HttpPost("create")]
        public Assessment Create(AssessmentCreateModel createModel)
        {
            createModel.CreatedTime = DateTime.Now;
            var assessment = assessmentApplication.CreateAssessment(createModel);
            return assessment;
        }
        /// <summary>
        /// 更新一个考核
        /// </summary>
        /// <param name="assessmentModel">考核模型,id不能为空</param>
        /// <returns></returns>
        [HttpPost("update")]
        public Assessment UpdateSubjects(AssessmentUpdateModel assessmentModel)
        {
            var assessment = assessmentApplication.UpdateSubjects(assessmentModel);
            return assessment;
        }
        /// <summary>
        /// 获取当前用户的所有考核
        /// </summary>
        /// <returns></returns>
        [HttpGet("getlist")]
        public ActionResult<SearchPageVO<Assessment>> GetList(string departmentID = null, int pageSize = 15, int pageCurrent = 1)
        {
            //根据claims获取用户所在部门,再获取考核
            User user = GetUserInfo();
            if (user == null)
            {
                return StatusCode(401);
            }
            return assessmentApplication.GetList(departmentID, pageSize, pageCurrent);
        }


        /// <summary>
        /// 删除考核
        /// </summary>
        /// <param name="assessmentId">id</param>
        /// <returns></returns>
        [HttpGet("delete")]
        public void Delete(string assessmentId)
        {
            assessmentApplication.Delete(assessmentId);
        }

    }
}