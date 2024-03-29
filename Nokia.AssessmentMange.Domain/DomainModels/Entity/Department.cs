﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Department : EntityBase
    {

        protected Department() { }


        public Department(string name, Department parent)
        {
            this.Name = name;
            if (parent != null)
            {
                this.Parent = parent;
                this.ParentId = parent.Id;
            }
        }

        [MaxLength(100)]
        public string Name { get; protected set; }
        public string ParentId { get; set; }
        public Department Parent { get; protected set; }
        [NotMapped]
        public IList<Department> Children { get; set; }
        /// <summary>
        /// 部门状态
        /// 1 存在 2 删除
        /// </summary>
        public int State { get; set; } = 1;
    }
}
