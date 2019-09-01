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
        public static SubjectConversion Null
        {
            get { return new SubjectConversion(); }
        }
        protected SubjectConversion() { }
        public SubjectConversion(Sex sex, ConversionTable conversionTable)
        {
            this.Sex = sex;

            this.ConversionTable = conversionTable;
        }
        public Sex Sex { get; set; }

        public ConversionTable ConversionTable { get; protected set; }

      

    }

}
