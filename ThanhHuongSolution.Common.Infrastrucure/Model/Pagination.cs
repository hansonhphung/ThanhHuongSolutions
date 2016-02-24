using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.Model
{
    public class Pagination
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortBy { get; set; }

        // true is ASC, false is DESC
        public bool SortDirection { get; set; }

        public Pagination() { }

        public Pagination(int pageIndex, int pageSize, string sortBy, bool sortDirection)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
    }
}
