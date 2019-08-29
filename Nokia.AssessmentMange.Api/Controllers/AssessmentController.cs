using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.DomainModels;
namespace Nokia.AssessmentMange.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        IAssessmentApplication assessmentApplication;
        Models.DtoMapper.IAssessmentMapper assessmentMapper;
        public AssessmentController(IAssessmentApplication assessmentApplication, Models.DtoMapper.IAssessmentMapper assessmentMapper)
        {
            this.assessmentApplication=assessmentApplication;
            this.assessmentMapper=assessmentMapper;
        }
        /// <summary>
        /// 创建一个考核
        /// </summary>
        /// <param name="assessmentModel">考核模型,id为空</param>
        /// <returns></returns>
        [HttpPost("create")]
        public Assessment Create(AssessmentModel assessmentModel)
        { 
            var assessment=assessmentMapper.ToEntity(assessmentModel);
            assessmentApplication.CreateAssessment(assessment);
            return assessment;
        }
        /// <summary>
        /// 更新一个考核
        /// </summary>
        /// <param name="assessmentModel">考核模型,id不能为空</param>
        /// <returns></returns>
        [HttpPost("update")]
        public Assessment UpdateSubjects(AssessmentModel assessmentModel)
        {
         throw new NotImplementedException();
        }

        [HttpGet("getlist")]
        public IEnumerable<Assessment> GetList()
        { 
            //根据claims获取用户所在部门,再获取考核
            throw new NotImplementedException();
            }

    }
}