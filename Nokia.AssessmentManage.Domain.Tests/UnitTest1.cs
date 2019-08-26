using Nokia.AssessmentMange.Domain.Infrastructure;
using System;
using Xunit;

namespace Nokia.AssessmentManage.Domain.Infrastructure.Tests
{
    public class CodeRunnerTest
    {
        [Fact]
        public void RunCode()
        {
            CodeRunner runner=new CodeRunner();
            var result=  runner.RunCode("return 1+1;");
            Assert.Equal("2",result);
              result = runner.RunCode("return 32m/15m;");
            Assert.Equal("2.1333333333333333333333333333", result);

        }
    }
}
