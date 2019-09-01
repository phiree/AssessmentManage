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
        public ConversionTable InitConversion(string subjectId, Sex sex, AgeRange ageRange, double score)
        { 
             Subject subject=subjectRepository.Get(subjectId);
           
            var table= new ConversionTable().Init(new List<AgeRange> { ageRange }, new List<double> { score });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    sex, table
                    
                    ));
            //subjectRepository.Update(subject);
            subjectRepository.SaveChanges();
            return table;
            }
        public ConversionTable AddScore(string subjectId, Sex sex, double score)
        {

            //memo: System.InvalidOperationException:“The property 'ConversionCellScore' on entity type 'AgeRange' is part of a key and so cannot be modified or marked as modified. To change the principal of an existing entity with an identifying foreign key first delete the dependent and invoke 'SaveChanges' then associate the dependent with the new principal.”

            Subject subject = subjectRepository.Get(subjectId);

           var table= subject.GetSubjectConversion(sex).ConversionTable;
            table.AddScore(score);
         
            subjectRepository.SaveChanges();
            return table;
        }



    }
}
