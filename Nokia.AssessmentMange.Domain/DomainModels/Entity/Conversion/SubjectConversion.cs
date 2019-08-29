using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目的成绩换算对照表
    /// </summary>
    public class SubjectConversion
    {
        protected SubjectConversion() { }
        public SubjectConversion(Sex sex, IList<AgeConversion> ageConversions)
        { 
             this.Sex=sex;
            
            this.AgeConversions=ageConversions;
            }
        public Sex Sex { get; set; }
        
        public IList<AgeConversion> AgeConversions { get; protected set; }
       

    }
}
