using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models.DtoMapper
{
    public class PersonAssessementGradeMapper : IPersonAssessementGradeMapper
    {
        public PersonAssessmentGrade ToEntity(PersonAssessementGradeCreateModel createModel)
        {
            bool isNew=!(createModel is PersonAssessementGradeUpdateModel);
            //  convert to entity
            throw new NotImplementedException();
        }
    }
}
