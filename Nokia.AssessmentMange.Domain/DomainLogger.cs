using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nokia.AssessmentMange.Domain
{
   public class DomainLogger
    {
      
        public static void Log(ILogger logger, string message, [CallerMemberName] string caller="")
        {
            logger.LogInformation($"Caller_{caller}:{ message}");
        }
    }
}
