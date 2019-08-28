using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.Application;
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
        
    }
}