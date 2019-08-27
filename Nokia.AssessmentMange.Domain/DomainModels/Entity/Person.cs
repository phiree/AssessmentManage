using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Person:Entity.EntityBase
    {
        protected Person() { }
        public string RealName { get;protected set;}
        public string Birthday { get;protected set;}
        public Sex Sex { get;protected set;}

        public string DepartmentId { get;set;}
        public Department Department { get;protected set;}

    }
    public enum Sex
    { 
        Male=1,
        Female=2
        }
}
