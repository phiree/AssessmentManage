using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.DomainModels;
namespace Nokia.AssessmentMange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        IAssessmentApplication assessmentApplication;
        public AssessmentController(IAssessmentApplication assessmentApplication)
        {
            this.assessmentApplication=assessmentApplication;
        }
        
        [HttpPost("create")]
        public Assessment Create(Assessment assessment)
        { 
            assessmentApplication.CreateAssessment(assessment);
            return assessment;
            }
        
    }
}