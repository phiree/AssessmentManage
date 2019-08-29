﻿using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using Nokia.AssessmentMange.Domain.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class PersonGradeRepository:EFCRepository<PersonGrade>,IPersonGradeRepository
    {
        public PersonGradeRepository(AssessmentDbContext conn) : base(conn) { }

        public IEnumerable<PersonGrade> FindByAssessmentAndPerson(string assessmentId, string personId)
        {
           throw new NotImplementedException();
        }

        public void SavePersonGrades(PersonGrade personGrade)
        { 
             
         }
    }
}