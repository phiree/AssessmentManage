using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.Infrastructure.EFC;
namespace Nokia.AssessmentMange.Domain.Infrastructure.Repository.Dapper
{
    public class PersonGradeRepository:BaseRepository<PersonGrade>,IPersonGradeRepository
    {
        public PersonGradeRepository(string conn) : base(conn) { }

        public IEnumerable<PersonGrade> FindByAssessmentAndPerson(string assessmentId, string personId)
        {
           throw new NotImplementedException();
        }

        public void SavePersonGrades(PersonGrade personGrade)
        { 
             
         }
    }
}
