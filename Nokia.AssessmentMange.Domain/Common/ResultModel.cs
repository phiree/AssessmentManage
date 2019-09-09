using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Common
{
    public class ResultModel<T>
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public enum ResultCode
    {
        success = 200,
        error = 400
    }
}
