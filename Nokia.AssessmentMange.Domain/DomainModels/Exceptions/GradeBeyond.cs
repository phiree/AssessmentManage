using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
   public class GradeBeyondMaximum:Exception
    {
        double grade;
        double maxGradeInConversion;
        public GradeBeyondMaximum(double grade,double maxGradeInConversion)
        { this.grade=grade;this.maxGradeInConversion=maxGradeInConversion;}
        public override string Message => $"成绩[{grade}]超出最大值[{maxGradeInConversion}]";
    }
    public class GradeBeyondMinimum : Exception
    {
        double grade;
        double minGradeInConversion;
        public GradeBeyondMinimum(double grade, double minGradeInConversion)
        { this.grade = grade; this.minGradeInConversion = minGradeInConversion; }
        public override string Message => $"成绩[{grade}]超出最小值[{minGradeInConversion}]";
    }
}
