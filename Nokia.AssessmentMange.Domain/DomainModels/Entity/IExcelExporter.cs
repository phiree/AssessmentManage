using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    public interface IExcelExporter
    {
        System.IO.MemoryStream ExportStream(DataSet ds);
    }
}
