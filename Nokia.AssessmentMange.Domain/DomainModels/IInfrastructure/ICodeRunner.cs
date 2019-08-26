using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure
{
    public interface ICodeRunner
    {
        string RunCode(string code);
    }
}
