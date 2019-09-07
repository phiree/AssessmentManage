using System.Collections.Generic;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper
{
    public interface ISubjectGradeMapper
    {
        // SubjectGrade ToEntity(SubjectGradeModel subjectGradeModel, Person person);
        IList<SubjectGrade> ToEntityList(IList<SubjectGradeModel> subjectGradeModels, Person person);
    }
}