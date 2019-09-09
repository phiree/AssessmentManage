using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nokia.AssessmentMange.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class AssessmentRepository : EFCRepository<Assessment>, IAssessmentRepository
    {
        AssessmentDbContext db;
        public AssessmentRepository(AssessmentDbContext db) : base(db)
        { }

        public List<Assessment> GetList(string departmentID, int pageSize, int pageCurrent, out int rowCount)
        {
            var where = PredicateBuilder.True<Assessment>();
            if (!string.IsNullOrEmpty(departmentID))
            {
                where = where.And(item => item.DepartmentId == departmentID);
            }
            rowCount = Conn.Assessments.Count(where);
            return Conn.Assessments.Where(where).Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
        }

        public Assessment GetAssessment(string assessmentID)
        {
            return Conn.Assessments.FirstOrDefault(i => i.Id == assessmentID);
        }


    }
}
