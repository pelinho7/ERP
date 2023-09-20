using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Appilcation.Features.Contractors
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int count, int pageNumber, int pageSize)
        {
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Page = Math.Min(pageNumber, TotalPages);
            TotalCount = count;
            PageSize = pageSize;
        }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
