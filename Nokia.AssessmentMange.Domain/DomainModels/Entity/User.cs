using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 系统用户
    /// </summary>
   public  class User:EntityBase
    {
        protected User() { }
        public string LoginName { get;protected set;}
        public string Password { get;protected set;}
        /// <summary>
        /// 关联的人员
        /// </summary>
        
        public Person Person { get;protected set;}
        public string PersonId { get;protected set;}
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get;protected set;}
    }
}
