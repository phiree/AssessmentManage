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
    public class SubjectController : ControllerBase
    {
        ISubjectApplication subjectApplication;
        public SubjectController(ISubjectApplication subjectApplication)
        {
            this.subjectApplication = subjectApplication;
        }
        [HttpPost("create")]
        public ActionResult<string> Create( Subject subject)
        {
            subjectApplication.Create(subject);
            return "OK";
            }
        [HttpGet]
        public ActionResult<string> Get(string id)
        {
            
            return "OK2";
        }


    }
}