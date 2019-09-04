using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Application.Dtos
{
    public class UserSearchVO
    {
        public int PageSize { get; set; }
        public int PageCurrent { get; set; }
        public int RowCount { get; set; }

        public List<User> UserList { get; set; }
    }
}
