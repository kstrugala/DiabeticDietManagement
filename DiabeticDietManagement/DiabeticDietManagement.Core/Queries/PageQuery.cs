using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Queries
{
    public class PageQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
