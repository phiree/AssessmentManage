using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;

namespace Nokia.AssessmentMange.Domain.Application
{
   public  class PersonAssessmentGradeApplication
    {
        IPersonAssessementGradeMapper personAssessementGradeMapper;
        IRepository<PersonAssessmentGrade> personAssessementGradeRepository;
        public PersonAssessmentGradeApplication(IPersonAssessementGradeMapper personAssessementGradeMapper,
             IRepository<PersonAssessmentGrade> personAssessementGradeRepository)
        { 
            this.personAssessementGradeMapper=personAssessementGradeMapper;
            this.personAssessementGradeRepository=personAssessementGradeRepository;
            }
        public PersonAssessmentGrade Create(PersonAssessementGradeCreateModel createModel)
        { 
            PersonAssessmentGrade  personAssessmentGrade= personAssessementGradeMapper.ToEntity(createModel);
            personAssessementGradeRepository.Insert(personAssessmentGrade);
            return personAssessmentGrade;
            }
        public PersonAssessmentGrade Update(PersonAssessementGradeUpdateModel updateModel)
        {
            PersonAssessmentGrade personAssessmentGrade = personAssessementGradeMapper.ToEntity(updateModel);
            personAssessementGradeRepository.Update(personAssessmentGrade);
            return personAssessmentGrade;
        }
    }
}
