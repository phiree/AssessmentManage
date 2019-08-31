﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
   public   class Department:EntityBase
    {
        
        protected Department() { }
        public Department(string name,string parentId)
        { 
            this.Name=name;
            this.ParentId=ParentId;
            }
        public string Name { get;protected set;}
        public string ParentId { get;set;}
        public Department Parent { get;protected set;}
        public IList<Department> Children { get;protected set;}
    }
}
