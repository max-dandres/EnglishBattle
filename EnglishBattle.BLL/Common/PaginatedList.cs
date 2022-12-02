using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.Common
{
    public static class PaginatedListIQueryableExtension
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, pageIndex, pageSize, count);
        }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex < TotalPages - 1;

        public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            this.AddRange(items);
        }
    }
}
