using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using Nokia.AssessmentMange.Domain.DomainModels.Service;

namespace Nokia.AssessmentMange.Domain.Application
{
    public class SubjectApplication : ApplicationBase<Subject>, ISubjectApplication
    {
        ISubjectService subjectService;
        ISubjectRepository subjectRepository;
        public SubjectApplication(ISubjectRepository subjectRepository, ISubjectService subjectService)
            : base(subjectRepository)
        {
            this.subjectService=subjectService;
            this.subjectRepository = subjectRepository;
        }
        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        { 
           return subjectService.Create(  name,   subjectType,   sexLimitation,   isQualifiedConversion,   unit);
        }
        public Subject CreateComputedSubject(string name, SubjectType subjectType,
            SexLimitation sexLimitation, bool isQualifiedConversion, string unit,string formula,IList<ParamSubject> paramSubjects)
        {
           var computedSubject=new ComputedSubject(name,subjectType,sexLimitation,isQualifiedConversion,unit,paramSubjects,formula);
              subjectRepository.Insert(computedSubject);
            return computedSubject;
        }
        public Subject GetWithParamSubject(string id)
        { 
           return subjectRepository.GetWithParamSubjects(id);
            }

    }
}
