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

    [Route("api/[controller]")]
    [ApiController]
    public class PersonAssessmentGradeController : ControllerBase
    {
       PersonAssessmentGradeApplication personAssessmentGradeApplication;
        public PersonAssessmentGradeController(PersonAssessmentGradeApplication personAssessmentGradeApplication)
        {
            this.personAssessmentGradeApplication= personAssessmentGradeApplication;

        }
        [HttpPost("create")]
        public PersonAssessmentGrade Create(PersonAssessementGradeCreateModel createModel)
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