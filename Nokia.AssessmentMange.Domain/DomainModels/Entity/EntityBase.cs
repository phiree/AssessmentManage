using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public interface IEntity<PrimaryKey> { 
        PrimaryKey Id { get;set;}
        }
    public class EntityBase:IEntity<string>
    {
        public EntityBase() {this.Id=Guid.NewGuid().ToString();  }
      
        public string Id { get;set;}

        
    }
}
