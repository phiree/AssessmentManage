using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class AgeNotSuitable:Exception
    {
        int age;
        string subjectName;
        Sex sex;
            
        public AgeNotSuitable(int age,string subjectName,Sex sex)
        { 
            this.age=age;
            this.subjectName=subjectName;
            this.sex=sex;
            }
        public override string Message =>$"没有在科目[{subjectName}]找到适合该年龄[{age}]及性别[{sex.ToString()}]的对照项";
    }
}
