using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    public class SearchPageVO<T>
    {
        public int PageSize { get; set; }
        public int PageCurrent { get; set; }
        public int RowCount { get; set; }

        public List<T> DataList { get; set; }
    }
}
