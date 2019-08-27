using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class NotFoundInRepository : Exception
    {
        string conditionStr; Type targetType;
        public NotFoundInRepository(IDictionary<string, object> conditions, Type targetType)
        {
            conditionStr = string.Join(";", conditions.Select(x => $"key:{x.Key},value:{x.Value}"));
            this.targetType = targetType;
        }
        public override string Message => $"没有找到对应的数据.查询对象:{targetType},条件:{conditionStr}";
    }
}
