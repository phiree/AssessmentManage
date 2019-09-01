using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public interface IEntity<PrimaryKey> { 
        PrimaryKey Id { get;set;}
        }
    public class EntityBase:IEntity<string>
    {
        public EntityBase() {this.Id=Guid.NewGuid().ToString();  }
      
        [MaxLength(100)]
        public string Id { get;set;}

        
    }
}
