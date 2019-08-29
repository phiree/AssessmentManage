using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Api.Models.DtoMapper;
namespace Nokia.AssessmentMange.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonAssessmentGradeController : ControllerBase
    {
        IPersonAssessementGradeMapper personAssessementGradeMapper;
        public PersonAssessmentGradeController(IPersonAssessementGradeMapper personAssessementGradeMapper)
        {
            this.personAssessementGradeMapper = personAssessementGradeMapper;

        }
        [HttpPost("create")]
        public PersonAssessmentGrade Create(PersonAssessementGradeCreateModel createModel)
        {
            var personAssessmentGrade=personAssessementGradeMapper.ToEntity(createModel);

            throw new NotImplementedException();

        }
        [HttpPost("update")]
        public PersonAssessmentGrade Update(PersonAssessementGradeUpdateModel updateModel)
        {
            var personAssessmentGrade = personAssessementGradeMapper.ToEntity(updateModel);

            throw new NotImplementedException();

        }
    }
}