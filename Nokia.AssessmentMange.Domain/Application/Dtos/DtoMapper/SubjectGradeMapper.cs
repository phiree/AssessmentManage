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
    public class SubjectGradeMapper : ISubjectGradeMapper
    {

        IRepository<PersonAssessmentGrade> personAssessmentGradeRepository;
        IRepository<Person> personRepository;
        IRepository<Assessment> assessmentRepository;
        IRepository<Subject> subjectRepository;
        public SubjectGradeMapper(IRepository<PersonAssessmentGrade> personAssessmentGradeRepository
            , IRepository<Person> personRepository, IRepository<Assessment> assessmentRepository,
            IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
            this.assessmentRepository = assessmentRepository;
            this.personRepository = personRepository;
            this.personAssessmentGradeRepository = personAssessmentGradeRepository;
        }

        public SubjectGrade ToEntity(SubjectGradeModel subjectGradeModel)
        {


            var subject = subjectRepository.Get(subjectGradeModel.SubjectId);
            var grade = new SubjectGrade(subject, subjectGradeModel.Grade);
            return grade;

        }
        public IList<SubjectGrade> ToEntityList(IList<SubjectGradeModel> subjectGradeModels)
        {
            IList<SubjectGrade> subjectGrades = new List<SubjectGrade>();

            foreach (var model in subjectGradeModels)
            {
                subjectGrades.Add(ToEntity(model));
            }
            return subjectGrades;

        }


    }
}
