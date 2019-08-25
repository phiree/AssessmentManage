using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Nokia.AssessmentMange.Domain.DomainModels;
namespace Nokia.AssessmentMange.Domain.Infrastructure
{
    public class ExcelExporter : IExcelExporter
    {
        public MemoryStream ExportStream(DataSet ds)
        {
            DataExport tt = new DataExport(ds, 0, false);
            tt.CreateWorkBook();
            MemoryStream ms = new MemoryStream();
            tt.Book.Write(ms);
            ms.Position = 0;

            return ms;
        }
    }
}
