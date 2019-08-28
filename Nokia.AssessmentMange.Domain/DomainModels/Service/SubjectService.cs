using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public class SubjectService : ISubjectService
    {
        ISubjectRepository subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            var newSubject = new Subject(name, subjectType, sexLimitation, isQualifiedConversion, unit);
            subjectRepository.Insert(newSubject);
            return newSubject;

        }
    }
}
