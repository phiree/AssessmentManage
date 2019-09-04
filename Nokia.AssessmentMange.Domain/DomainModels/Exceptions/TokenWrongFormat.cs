using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Exceptions
{
    public class TokenWrongFormat : Exception
    {
        private string _token;
        public TokenWrongFormat(string token)
        {
            this._token = token;
        }
        public override string Message => $"验证[{_token}]失效";
    }
}
