using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class PersonGradeRepository : EFCRepository<PersonAssessmentGrade>, IPersonGradeRepository
    {
        public PersonGradeRepository(AssessmentDbContext conn) : base(conn) { }

        public IEnumerable<PersonAssessmentGrade> FindByAssessmentAndPerson(string assessmentId, string personId)
        {
            throw new NotImplementedException();
        }

        public new void Get(string Id)

        {

        }

        public PersonAssessmentGrade GetByPersonAssessment(string personId, string assessmentId)
        {
            return Conn.PersonGrades
                   .Include(x => x.AssessmentGrades)
                    .ThenInclude(y => y.SubjectGrades)
                        .ThenInclude(z => z.Subject)
                   .FirstOrDefault(item => item.PersonId == personId && item.AssessmentId == assessmentId);
        }

        public IEnumerable<PersonAssessmentGrade> GetByPersonAssessment(string assessmentId)
        {
            return Conn.PersonGrades
                .Include(i => i.Person)
                .Include(x => x.AssessmentGrades)
                    .ThenInclude(y => y.SubjectGrades)
                        .ThenInclude(z => z.Subject)
                .Where(item => item.AssessmentId == assessmentId);
        }


        public int GetCountByAssessment(string assessmentId)
        {
            return Conn.PersonGrades.Count(item => item.AssessmentId == assessmentId);
        }

    }
}
