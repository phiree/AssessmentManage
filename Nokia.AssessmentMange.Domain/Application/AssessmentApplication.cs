﻿using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System.Linq;

namespace Nokia.AssessmentMange.Domain.Application
{
    /// <summary>
    /// 考核管理
    /// </summary>
    public class AssessmentApplication : IAssessmentApplication
    {
        IAssessmentService assessmentService;
        IRepository<Assessment> assessmentRepository;
        ISubjectRepository subjectRepository;
        IRepository<Person> personSubject;
        IPersonGradeRepository personGradeRepository;
        public AssessmentApplication(IAssessmentService assessmentService,
            IRepository<Assessment> assessmentRepository,
            IRepository<Person> personSubject,
        ISubjectRepository subjectRepository,
        IPersonGradeRepository personGradeRepository)
        {
            this.assessmentRepository = assessmentRepository;
            this.assessmentService = assessmentService;
            this.subjectRepository = subjectRepository;  
            this.personSubject=personSubject;
            this.personGradeRepository=personGradeRepository;
        }
        public Assessment CreateAssessment(Assessment assessment)
        { 
            var ass=new Assessment(assessment.DepartmentId,assessment.Name, assessment.Annual);
            assessmentRepository.Insert(assessment);
            return assessment;

            }
        public IEnumerable<Assessment> GetAllAssessment()
        { 
            var list=assessmentRepository.GetAll();
            return list;                                                                                    
            }
        public PersonGrade SavePersonGrade(string assessmentId,string personId,
            bool isMakeup, IDictionary<string, double> grades)
        {
           var assessment= assessmentRepository.Get(assessmentId);
            var person=personSubject.Get(personId);
           var subjects= subjectRepository.GetList(grades.Keys);
           var realSubjectGrades =subjects.Select(x=>new SubjectGrade(x, grades[x.Id]));

            assessmentService.SavePersonGrade(assessment,person,false,isMakeup, realSubjectGrades);
            throw new NotImplementedException();
        }
        public PersonGrade GetPersonGrade(string assessmentId,string personId)
        { 
           throw new NotImplementedException();
            }

    }
}
