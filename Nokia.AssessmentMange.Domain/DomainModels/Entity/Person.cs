using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Person : EntityBase
    {
        public Person() { }
        public string RealName { get; set; }
        public DateTime Birthday { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }

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
        public string Position { get; set; }

        public Sex Sex { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string IdNo { get; set; }
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
