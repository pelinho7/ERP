using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.Companies
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Pagination = new Pagination(count, pageNumber, pageSize);
            Items = items;
        }

        public IEnumerable<T> Items { get; set; }
        public Pagination Pagination { get; set; }
    }
}
