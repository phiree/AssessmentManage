 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Nokia.AssessmentMange.Domain.DomainModels.IInfrastructure;

namespace Nokia.AssessmentMange.Domain.Infrastructure
{
    public class CodeRunner : ICodeRunner
    {
        public string RunCode(string code)
        {
            var result = CSharpScript.EvaluateAsync( code) .Result;
            return result.ToString();
        }
    }
}
