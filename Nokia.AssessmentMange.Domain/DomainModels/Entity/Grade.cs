using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels 
{
    /// <summary>
    /// 成绩. 成绩为0
    /// </summary>
    public class Grade : IEquatable<Grade>,IComparable<Grade>
    {
        public static Grade NullGrade
        {
            get { return new Grade(0); }
        }
        public Grade() { }
        
        public double GradeValue { get; protected set; } 
        public Grade(double? gradeValue)
        {
            if(!gradeValue.HasValue)
            { gradeValue=0;
                }
            GradeValue = gradeValue.Value;
        }
        public override string ToString()
        {
            return GradeValue==0?"":GradeValue.ToString("{0:2}");
        }

        public bool Equals(Grade other)
        {
           return this==other;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if(obj is Grade)
            { 
                return ((Grade)obj)==this;
                }
           return false;
        }

        public int CompareTo(Grade other)
        {
            return GradeValue.CompareTo(other.GradeValue);
        }

        public  static  bool operator == (Grade grade1,Grade grade2) {
            return grade1.GradeValue==grade2.GradeValue;
            }
        public static bool operator !=(Grade grade1, Grade grade2)
        {
            return grade1.GradeValue != grade2.GradeValue;
        }
        public static bool operator >(Grade grade1, Grade grade2)
        {
            return grade1.GradeValue > grade2.GradeValue;
        }
        public static bool operator <(Grade grade1, Grade grade2)
        {
            return grade1.GradeValue < grade2.GradeValue;
        }
        public static bool operator >=(Grade grade1, Grade grade2)
        {
            return grade1.GradeValue >= grade2.GradeValue;
        }
        public static bool operator <=(Grade grade1, Grade grade2)
        {
            return grade1.GradeValue <= grade2.GradeValue;
        }
    }
}
