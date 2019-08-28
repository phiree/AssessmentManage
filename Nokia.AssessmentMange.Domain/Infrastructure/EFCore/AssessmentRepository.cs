using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Infrastructure.EFCore
{
    public class AssessmentRepository
    {
        AssessmentDbContext db;
        public AssessmentRepository(AssessmentDbContext db)
        { }
    }
}
