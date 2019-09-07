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
            this.subjectService = subjectService;
            this.subjectRepository = subjectRepository;
        }
        public Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            return subjectService.Create(name, subjectType, sexLimitation, isQualifiedConversion, unit);
        }
        public Subject CreateComputedSubject(string name, SubjectType subjectType,
            SexLimitation sexLimitation, bool isQualifiedConversion, string unit, string formula, IList<ParamSubject> paramSubjects)
        {
            var computedSubject = new ComputedSubject(name, subjectType, sexLimitation, isQualifiedConversion, unit, paramSubjects, formula);
            subjectRepository.Insert(computedSubject);
            return computedSubject;
        }
        public Subject GetWithParamSubject(string id)
        {
            return subjectRepository.GetWithParamSubjects(id);
        }
        public ConversionTable InitConversion(string subjectId, Sex sex, AgeRange ageRange, int score)
        {
            Subject subject = subjectRepository.Get(subjectId);

            var table = new ConversionTable().Init(new List<AgeRange> { ageRange }, new List<int> { score });
            subject.SubjectConversions.Add(
                new SubjectConversion(
                    sex, table
                    ));
            //subjectRepository.Update(subject);
            subjectRepository.SaveChanges();
            return table;
        }

        public ConversionTable GetConversionTable(string subjectId, Sex sex)
        {
            Subject subject = subjectRepository.Get(subjectId);
            ConversionTable table = null;
            if (subject.SubjectConversions == null || subject.SubjectConversions.Count == 0)
            {
                table = null;
            }
            else
            {
                table = subject.GetSubjectConversion(sex).ConversionTable;
            }
            return table;
        }


        public ConversionTable AddScore(string subjectId, Sex sex, double score)
        {
            Subject subject = subjectRepository.Get(subjectId);

            var table = subject.GetSubjectConversion(sex).ConversionTable;
            table.AddScore(score);

            subjectRepository.SaveChanges();
            return table;
        }

        public ConversionTable AddAgeRange(string subjectId, Sex sex, AgeRange ageRange)
        {
            Subject subject = subjectRepository.Get(subjectId);
            var table = subject.GetSubjectConversion(sex).ConversionTable;
            table.AddAgeRange(ageRange);
            subjectRepository.SaveChanges();
            return table;
        }

        public ConversionTable RemoveScore(string subjectId, Sex sex, double score)
        {
            Subject subject = subjectRepository.Get(subjectId);

            var table = subject.GetSubjectConversion(sex).ConversionTable;
            table.RemoveScore(score);
            subjectRepository.SaveChanges();
            return table;
        }

        public ConversionTable RemoveAgeRange(string subjectId, Sex sex, AgeRange ageRange)
        {
            Subject subject = subjectRepository.Get(subjectId);
            var table = subject.GetSubjectConversion(sex).ConversionTable;
            table.RemoveAge(ageRange);
            subjectRepository.SaveChanges();
            return table;
        }

        public ConversionTable SetGrade(string subjectId, Sex sex, AgeRange ageRange, double score, double grade)
        {
            Subject subject = subjectRepository.Get(subjectId);
            var table = subject.GetSubjectConversion(sex).ConversionTable;
            table.SetGrade(ageRange, score, grade);
            subjectRepository.SaveChanges();
            return table;
        }
    }
}
