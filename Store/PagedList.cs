using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> source, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            var result = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            FilteredCount = result.Count();

            this.AddRange(result.ToList());
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; }

        public int FilteredCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; }
    }
}
