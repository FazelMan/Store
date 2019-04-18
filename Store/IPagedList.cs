using System.Collections;
using System.Collections.Generic;

namespace Store
{
    public interface IPagedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int FilteredCount { get; }
        int TotalPages { get; }
    }
}
