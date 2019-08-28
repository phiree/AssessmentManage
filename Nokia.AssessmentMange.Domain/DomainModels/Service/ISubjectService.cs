namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    public interface ISubjectService
    {
        Subject Create(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit);
    }
}