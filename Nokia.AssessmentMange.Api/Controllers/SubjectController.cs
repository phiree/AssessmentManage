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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        ISubjectApplication subjectApplication;
        public SubjectController(ISubjectApplication subjectApplication)
        {
            this.subjectApplication = subjectApplication;
        }
        [HttpPost("create")]
        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            return subjectApplication.Create(name, subjectType, sexLimitation, isQualifiedConversion, unit);

        }
        [HttpGet("Get")]
        public Subject Get(string subjectId)
        {

            return subjectApplication.Get(subjectId);
        }
        [HttpGet("getall")]
        public IEnumerable<Subject> GetAll()
        {
            return subjectApplication.GetAll();
        }
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


    }
}