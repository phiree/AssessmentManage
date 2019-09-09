using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Person : EntityBase
    {

        public Person() { }
        public Person(string id, string realName, DateTime birthday, Sex sex)
        {
            this.Id = id;
            this.RealName = realName;
            this.Birthday = birthday;
            this.Sex = sex;
        }


        [MaxLength(20)]
        public string RealName { get; set; }
        public DateTime Birthday { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public User User { get; set; }

        public int Age
        {
            get
            {// Save today's date.
                var today = DateTime.Today;
                // Calculate the age.
                var age = today.Year - Birthday.Year;
                // Go back to the year the person was born in case of a leap year
                if (Birthday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        /// <summary>
        /// 军衔
        /// </summary>
        public MilitaryRank MilitaryRank { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [MaxLength(50)]
        public string Position { get; set; }

        public Sex Sex { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        [MaxLength(50)]
        public string IdNo { get; set; }
        /// <summary>
        /// 人员状态
        /// 1 存在 2 删除
        /// </summary>

        public int State { get; set; } = 1;
    }
    public enum Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        Female = 2
    }
}
