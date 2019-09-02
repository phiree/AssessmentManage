using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public class Person : EntityBase
    {
        protected Person() { }
        public Person(string realName,DateTime birthday,Sex sex,string departmentId)
        { 
            this.RealName=realName;
            this.Birthday=birthday;
            this.Sex=sex;
            this.DepartmentId=departmentId;
            }
        public string RealName { get; protected set; }
        public DateTime Birthday { get; protected set; }
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
        public Sex Sex { get; protected set; }

        public string DepartmentId { get; set; }
        public Department Department { get; protected set; }

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
