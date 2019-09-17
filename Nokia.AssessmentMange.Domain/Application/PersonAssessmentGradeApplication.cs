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
    public class PersonAssessmentGradeApplication : IPersonAssessmentGradeApplication
    {
        ISubjectGradeMapper subjectGradeMapper;
        IPersonGradeRepository personAssessementGradeRepository;
        IGradeCalculater gradeCalculater;

        public PersonAssessmentGradeApplication(ISubjectGradeMapper subjectGradeMapper,
             IPersonGradeRepository personAssessementGradeRepository,IGradeCalculater gradeCalculater)
        {
            this.subjectGradeMapper = subjectGradeMapper;
            this.personAssessementGradeRepository = personAssessementGradeRepository;
            this.gradeCalculater = gradeCalculater;
        }
        /// <summary>
        /// 人员考核成绩
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public PersonAssessmentGrade Get(string assessmentId, string personId)
        {
            PersonAssessmentGrade personAssessmentGrade = null;//  new PersonAssessmentGrade(assessmentId, personId)

            if (personAssessementGradeRepository.Find(x => x.AssessmentId == assessmentId && x.PersonId == personId).Count() == 0)
            {
                personAssessmentGrade = new PersonAssessmentGrade(assessmentId, personId);
                personAssessementGradeRepository.Insert(personAssessmentGrade);
            }
            else
            {
                personAssessmentGrade = personAssessementGradeRepository.GetByPersonAssessment(personId, assessmentId);
            }
            return personAssessmentGrade;
        }
        /// <summary>
        /// 录入成绩
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PersonAssessmentGrade CommitGrades(string personAssessmentGradeId, bool isAbsent, bool isMakeup, IList<SubjectGradeModel> subjectGradeModels)
        {

            var personAssessementGrade = personAssessementGradeRepository.GetEager(personAssessmentGradeId);
            var subjectGrades = subjectGradeMapper.ToEntityList(subjectGradeModels, personAssessementGrade.Person);

            personAssessementGrade.CommitGrade(new AssessmentGrade(isAbsent, isMakeup, subjectGrades),gradeCalculater);
            personAssessementGradeRepository.SaveChanges();
            return personAssessementGrade;

        }

        public IEnumerable<PersonAssessmentGrade> GetList(string assessmentId)
        {
            return personAssessementGradeRepository.GetByPersonAssessment(assessmentId);
        }

        public int GetCountByAssessment(string assessmentId)
        {
            return personAssessementGradeRepository.GetCountByAssessment(assessmentId);
        }


    }
}
