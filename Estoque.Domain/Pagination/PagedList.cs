using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            PageSize = pageSize;
            TotalCount = totalCount;
            AddRange(items);
        }
    }
}
