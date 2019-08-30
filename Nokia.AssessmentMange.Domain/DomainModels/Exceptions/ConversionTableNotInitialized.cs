using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class ConversionTableNotInitialized:Exception
    {
        public ConversionTableNotInitialized()
        { }
        public override string Message => $"换算表尚未初始化,不能添加年龄段或者分数.请先初始化";
    }
}
