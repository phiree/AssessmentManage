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
        public ActionResult<Subject> Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
           return subjectApplication.Create(name, subjectType, sexLimitation, isQualifiedConversion, unit);
            
            }
        [HttpGet]
        public ActionResult<string> Get(string id)
        {
            
            return "OK2";
        }


    }
}