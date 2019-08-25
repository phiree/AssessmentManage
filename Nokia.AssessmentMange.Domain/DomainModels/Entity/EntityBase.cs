using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Entity
{
    public class EntityBase
    {
        public EntityBase()
        { 
            Id=Guid.NewGuid().ToString();
            }
        public string Id { get;set;}
    }
}
