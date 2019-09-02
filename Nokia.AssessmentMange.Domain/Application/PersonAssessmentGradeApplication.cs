using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper;
using Nokia.AssessmentMange.Domain.Application.Dtos;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.Application
{
   public  class PersonAssessmentGradeApplication
    {
        IPersonAssessementGradeMapper personAssessementGradeMapper;
        IEFCRepository<PersonAssessmentGrade> personAssessementGradeRepository;
        public PersonAssessmentGradeApplication(IPersonAssessementGradeMapper personAssessementGradeMapper,
             IEFCRepository<PersonAssessmentGrade> personAssessementGradeRepository)
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
        /// <summary>
        /// 录入成绩
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonAssessmentGrade CommitGrades(PersonAssessementGradeCreateModel model)
        {
            PersonAssessmentGrade personAssessementGrade=null;
         var mayBePersonAssessementGrade=  personAssessementGradeRepository.Find(x=>x.AssessmentId==model.AssessmentId&&x.PersonId==model.PersonId);
           
            if(mayBePersonAssessementGrade.Count()==1)
            {
                personAssessementGrade=mayBePersonAssessementGrade.First();
               
                } 
            else
            {
                //personAssessementGrade= personAssessementGradeRepository.Insert(newone)


            } 
            //personAssessementGrade.CommitGrade();

            return personAssessementGrade;

        }
            
    }
}
