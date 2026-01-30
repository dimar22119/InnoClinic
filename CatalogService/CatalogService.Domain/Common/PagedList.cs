using System.Collections.ObjectModel;

namespace CatalogService.Domain.Common
{
    public class PagedList<T>(IList<T> items, int totalCount) : ReadOnlyCollection<T>(items)
    {
        public int TotalCount { get; } = totalCount;
    }
}
