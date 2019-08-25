using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain
{
    public interface IRepository
    {
        void WriteLogSample();
        dynamic GetSampleList(string connectionstring);
    }
}
