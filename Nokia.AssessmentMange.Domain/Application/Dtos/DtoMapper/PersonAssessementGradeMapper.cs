using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nokia.AssessmentMange.Domain.Application;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;

namespace Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper
{
    public class PersonAssessementGradeMapper : IPersonAssessementGradeMapper
    {

         IRepository<PersonAssessmentGrade> personAssessmentGradeRepository;
        IRepository<Person> personRepository;
        IRepository<Assessment> assessmentRepository;
        IRepository<Subject> subjectRepository;
        public PersonAssessementGradeMapper(IRepository<PersonAssessmentGrade> personAssessmentGradeRepository
            , IRepository<Person> personRepository, IRepository<Assessment> assessmentRepository,
            IRepository<Subject> subjectRepository)
        { 
            this.subjectRepository=subjectRepository;
            this.assessmentRepository=assessmentRepository;
            this.personRepository=personRepository;
            this.personAssessmentGradeRepository=personAssessmentGradeRepository;
            }
        public PersonAssessmentGrade ToEntity(PersonAssessementGradeCreateModel createModel)
        {
            bool isNew=!(createModel is PersonAssessementGradeUpdateModel);
           var person= personRepository.Get( createModel.PersonId);
            var assessment=assessmentRepository.Get(createModel.AssessmentId);
            PersonAssessmentGrade personAssessmentGrade=null;
           var grades=new List<SubjectGrade>();
            foreach(var gradeModel in createModel.Grades)
            {
                var subject=subjectRepository.Get(gradeModel.SubjectId);
                var grade=new SubjectGrade(subject,gradeModel.Grade);
                grade.Equals(grade);

            }
            if(isNew)
            { 
                personAssessmentGrade=new PersonAssessmentGrade(
                    assessment,person,createModel.IsAbsent,createModel.IsMakeup,grades);
                }
            else
            { 
                string id=((PersonAssessementGradeUpdateModel)createModel).Id;
                personAssessmentGrade=personAssessmentGradeRepository.Get(id);
                personAssessmentGrade.Update(assessment, person, createModel.IsAbsent, createModel.IsMakeup, grades);
                }
            //  convert to entity
            throw new NotImplementedException();
        }
        

        
    }
}
