using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class User : EntityBase
    {
        public User() { }

        public User(string loginName, bool isAdmin)
        {
            this.LoginName = loginName;
            this.IsAdmin = isAdmin;

        }

        public User(string id, string loginName, bool isAdmin) : this(loginName, isAdmin)
        {
            this.Id = id;
            this.LoginName = loginName;
            this.IsAdmin = isAdmin;
        }

        public string LoginName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 关联的人员
        /// </summary>

        public Person Person { get; set; }
        public string PersonId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [Column(TypeName = "bit")]
        public bool IsAdmin { get; set; }
    }
}
