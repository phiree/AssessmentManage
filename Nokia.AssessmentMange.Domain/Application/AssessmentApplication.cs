using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System.Linq;
using Nokia.AssessmentMange.Domain.Application.Dtos;

namespace Nokia.AssessmentMange.Domain.Application
{
    /// <summary>
    /// 考核管理
    /// </summary>
    public class AssessmentApplication : ApplicationBase<Assessment>, IAssessmentApplication
    {
        IAssessmentService assessmentService;
        IRepository<Assessment> assessmentRepository;
        ISubjectRepository subjectRepository;
        IRepository<Person> personSubject;
        IPersonGradeRepository personGradeRepository;
        Dtos.DtoMapper.IAssessmentMapper assemblyMapper;

        public AssessmentApplication(IAssessmentService assessmentService,
            IRepository<Assessment> assessmentRepository,
            IRepository<Person> personSubject,
        ISubjectRepository subjectRepository,
        IPersonGradeRepository personGradeRepository
            , Dtos.DtoMapper.IAssessmentMapper assemblyMapper) : base(assessmentRepository)
        {
            this.assessmentRepository = assessmentRepository;
            this.assessmentService = assessmentService;
            this.subjectRepository = subjectRepository;
            this.personSubject = personSubject;
            this.personGradeRepository = personGradeRepository;
            this.assemblyMapper = assemblyMapper;
        }
        public Assessment CreateAssessment(AssessmentCreateModel assessment)
        {
            var ass = assemblyMapper.ToEntity(assessment);// new Assessment(assessment.DepartmentId,assessment.Name, assessment.Annual);
            assessmentRepository.Insert(ass);
            return ass;

        }
        public IEnumerable<Assessment> GetAllAssessment()
        {
            var list = assessmentRepository.GetAll();
            return list;
        }
        public PersonAssessmentGrade SavePersonGrade(string assessmentId, string personId,
            bool isMakeup, IDictionary<string, double> grades)
        {
            var assessment = assessmentRepository.Get(assessmentId);
            var person = personSubject.Get(personId);
            var subjects = subjectRepository.GetList(grades.Keys);
            var realSubjectGrades = subjects.Select(x => new SubjectGrade(x, grades[x.Id]));

            assessmentService.SavePersonGrade(assessment, person, false, isMakeup, realSubjectGrades);
            throw new NotImplementedException();
        }
        public PersonAssessmentGrade GetPersonGrade(string assessmentId, string personId)
        {
            throw new NotImplementedException();
        }

        public Assessment UpdateSubjects(AssessmentModel assessmentModel)
        {
            var ass = assemblyMapper.ToEntity(assessmentModel);// new Assessment(assessment.DepartmentId,assessment.Name, assessment.Annual);
            assessmentRepository.Update(ass);
            return ass;
            throw new NotImplementedException();
        }
    }
}
